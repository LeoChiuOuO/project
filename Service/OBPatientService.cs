
// Services/OBPatientService.cs
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Models.Service
{
    public class OBPatientService : IOBPatientService
    {
        private readonly string _connectionString;
        private readonly ILogger<OBPatientService> _logger;

        public OBPatientService(IConfiguration configuration, ILogger<OBPatientService> logger)
        {
            _connectionString = configuration.GetConnectionString("ChiehConnection") 
                ?? throw new ArgumentNullException("Connection string not found");
            _logger = logger;
        }

        public async Task<List<OBPatientRecord>> GetPatientRecordsAsync(OBPatientSearchCriteria criteria)
        {
            var results = new List<OBPatientRecord>();

            try
            {
                var sql = GetPatientRecordsQuery();
                var parameters = CreateParameters(criteria);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(sql, connection);

                command.Parameters.AddRange(parameters);
                command.CommandTimeout = 120; // 2 minutes timeout

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    results.Add(MapToOBPatientRecord(reader));
                }

                _logger.LogInformation($"Retrieved {results.Count} OB patient records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving OB patient records");
                throw;
            }

            return results;
        }

        public async Task<Dictionary<string, int>> GetStatisticsAsync(OBPatientSearchCriteria criteria)
        {
            var statistics = new Dictionary<string, int>();

            try
            {
                var sql = GetStatisticsQuery();
                var parameters = CreateParameters(criteria);

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(sql, connection);

                command.Parameters.AddRange(parameters);
                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var key = reader.GetString("Category");
                    var count = reader.GetInt32("Count");
                    statistics[key] = count;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving statistics");
                statistics["錯誤"] = 0;
            }

            return statistics;
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Connection test failed");
                return false;
            }
        }

        public async Task<List<string>> GetAvailableCompaniesAsync()
        {
            var companies = new List<string>();

            try
            {
                var sql = "SELECT DISTINCT [Company] FROM [chieh].[dbo].[OB入住記錄檔] WHERE [Company] IS NOT NULL ORDER BY [Company]";

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(sql, connection);

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    companies.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving companies");
                // Return default company if error occurs
                companies.Add("民權");
            }

            return companies;
        }

        private string GetPatientRecordsQuery()
        {
            return @"
                WITH T1 AS (
                  SELECT 
                    A.[病歷號],
                    B.[姓名],
                    A.[床號],
                    A.[入住日期],
                    A.[離開日期],
                    C.[分娩方式],
                    A.[產後床號],
                    A.[產後床入住日期],
                    A.[產後床離開日期],
                    A.[原始入住日期],
                    A.[轉床否],
                    ROW_NUMBER() OVER (
                        PARTITION BY A.[病歷號]
                        ORDER BY A.[入住日期] DESC
                    ) AS rn
                  FROM [chieh].[dbo].[OB入住記錄檔] AS A
                  LEFT JOIN [chieh].[dbo].[OB病患檔] AS B
                    ON A.[病歷號] = B.[病歷號]
                  LEFT JOIN [chieh].[dbo].[OB生產記錄檔] AS C
                    ON A.[病歷號] = C.[病歷號]
                  WHERE
                    A.[入住日期] BETWEEN @StartDate AND @EndDate
                    AND A.[Company] = @Company
                    AND A.[床號] NOT LIKE '%NST%'
                    AND A.[床號] NOT LIKE N'%照光%'
                    AND (@PatientNumber IS NULL OR A.[病歷號] LIKE '%' + @PatientNumber + '%')
                    AND (@PatientName IS NULL OR B.[姓名] LIKE '%' + @PatientName + '%')
                )
                SELECT 
                  [病歷號],
                  [姓名],
                  [床號],
                  [入住日期],
                  [離開日期],
                  [分娩方式],
                  [產後床號],
                  [產後床入住日期],
                  [產後床離開日期],
                  [原始入住日期],
                  [轉床否]
                FROM T1
                WHERE rn = 1
                ORDER BY [入住日期] DESC";
        }

        private string GetStatisticsQuery()
        {
            return @"
                WITH T1 AS (
                  SELECT 
                    A.[病歷號],
                    C.[分娩方式],
                    A.[轉床否],
                    ROW_NUMBER() OVER (
                        PARTITION BY A.[病歷號]
                        ORDER BY A.[入住日期] DESC
                    ) AS rn
                  FROM [chieh].[dbo].[OB入住記錄檔] AS A
                  LEFT JOIN [chieh].[dbo].[OB病患檔] AS B
                    ON A.[病歷號] = B.[病歷號]
                  LEFT JOIN [chieh].[dbo].[OB生產記錄檔] AS C
                    ON A.[病歷號] = C.[病歷號]
                  WHERE
                    A.[入住日期] BETWEEN @StartDate AND @EndDate
                    AND A.[Company] = @Company
                    AND A.[床號] NOT LIKE '%NST%'
                    AND A.[床號] NOT LIKE N'%照光%'
                    AND (@PatientNumber IS NULL OR A.[病歷號] LIKE '%' + @PatientNumber + '%')
                    AND (@PatientName IS NULL OR B.[姓名] LIKE '%' + @PatientName + '%')
                )
                SELECT 
                  '總病患數' as Category, COUNT(*) as Count
                FROM T1 WHERE rn = 1
                UNION ALL
                SELECT 
                  ISNULL([分娩方式], '未記錄') + ' 分娩' as Category, COUNT(*) as Count
                FROM T1 WHERE rn = 1
                GROUP BY [分娩方式]
                UNION ALL
                SELECT 
                  CASE WHEN [轉床否] = 'Y' THEN '有轉床' ELSE '無轉床' END as Category, COUNT(*) as Count
                FROM T1 WHERE rn = 1
                GROUP BY [轉床否]";
        }

        private SqlParameter[] CreateParameters(OBPatientSearchCriteria criteria)
        {
            return new[]
            {
                new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = criteria.StartDate },
                new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = criteria.EndDate },
                new SqlParameter("@Company", SqlDbType.NVarChar) { Value = criteria.Company },
                new SqlParameter("@PatientNumber", SqlDbType.NVarChar) { Value = (object?)criteria.PatientNumber ?? DBNull.Value },
                new SqlParameter("@PatientName", SqlDbType.NVarChar) { Value = (object?)criteria.PatientName ?? DBNull.Value }
            };
        }

        private OBPatientRecord MapToOBPatientRecord(SqlDataReader reader)
        {
            return new OBPatientRecord
            {
                病歷號 = GetStringValue(reader, "病歷號"),
                姓名 = GetStringValue(reader, "姓名"),
                床號 = GetStringValue(reader, "床號"),
                入住日期 = GetDateTimeValue(reader, "入住日期"),
                離開日期 = GetDateTimeValue(reader, "離開日期"),
                分娩方式 = GetStringValue(reader, "分娩方式"),
                產後床號 = GetStringValue(reader, "產後床號"),
                產後床入住日期 = GetDateTimeValue(reader, "產後床入住日期"),
                產後床離開日期 = GetDateTimeValue(reader, "產後床離開日期"),
                原始入住日期 = GetDateTimeValue(reader, "原始入住日期"),
                轉床否 = GetStringValue(reader, "轉床否")
            };
        }

        private string GetStringValue(SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? string.Empty : reader.GetString(ordinal);
        }

        private DateTime? GetDateTimeValue(SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetDateTime(ordinal);
        }
    }
}
