namespace WebApplication_Dianthus.Models.Repository
{
    using Dapper;
    using Microsoft.Data.SqlClient;
    using WebApplication_Dianthus.Models.Interface;

    public class ChiehRepository : IChiehRepository
    {
        private readonly string _connectionString;

        public ChiehRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ChiehConnection");
        }

        public async Task<IEnumerable<ChiehDto>> GetDataAsync(string checkTime)
        {
            var sql = @"
                select A.Counter as H000, C.轉檢單位代號 as H001, C.處置檔_Counter as H002,
                    CONCAT(A.科別代碼, '.', F.代碼內容) as H003, B.姓名 as H004, A.就診日 as H005,
                    C.處置簡稱 as H006, '' as H007, E.病歷號碼 as H008,
                    CASE WHEN E.身份證字號 <> '' THEN E.身份證字號 WHEN E.護照號碼 <> '' THEN E.護照號碼 END as H009,
                    E.姓名 as H010,
                    CONCAT(CAST(CAST(LEFT(E.生日, 3) AS INT) + 1911 AS VARCHAR(4)), '-', SUBSTRING(E.生日, 4, 2), '-', SUBSTRING(E.生日, 6, 2)) AS H011,
                    CASE E.性別代碼 WHEN 0 THEN 'M' WHEN 1 THEN 'F' END as H012,
                    CONCAT('0', E.BBC) as H013,
                    I.週數 as H014,
                    CONCAT(CAST(CAST(LEFT(E.預產期, 3) AS INT) + 1911 AS VARCHAR(4)), '-', SUBSTRING(E.預產期, 4, 2), '-', SUBSTRING(E.預產期, 6, 2)) as H015,
                    isnull(G.身份證字號, '') as H016,
                    isnull(G.姓名, '') as H017,
                    CONCAT(CAST(CAST(LEFT(G.生日, 3) AS INT) + 1911 AS VARCHAR(4)), '-', SUBSTRING(G.生日, 4, 2), '-', SUBSTRING(G.生日, 6, 2)) AS H018,
                    CASE G.性別代碼 WHEN 0 THEN 'M' WHEN 1 THEN 'F' ELSE '' END as H019,
                    IIF(G.BBC<>'', CONCAT('0', G.BBC), '') as H020,
                    'O' AS H021,
                    '' As H022,
                    C.開醫令日期時間 as H023,
                    C.異動日期時間 as H024,
                    E.地址 as H025,
                    E.最後一次月經日期 as H026,
                    E.身高 as H027,
                    E.體重 as H028
                    from dbo.門診檔 as A
                    inner join dbo.[人事資料檔] as B on B.人事代號=A.醫師代號
                    inner JOIN dbo.[門診處置內容檔] as C on C.門診檔_Counter=A.Counter
                    inner join dbo.[處置檔] as D on C.處置檔_Counter=D.Counter
                    inner join dbo.[病患檔] as E on A.病患檔_Counter=E.Counter
                    inner join dbo.[代碼檔] as F on A.科別代碼=F.代碼 and F.代碼名稱='科別代碼'
                    left join dbo.[病患檔] as G on E.父或母counter=G.Counter
                    left join dbo.[門診檔] as H on H.病患檔_Counter=A.病患檔_Counter and H.就診日=A.就診日
                    left join dbo.[病患婦產科衛教檔] as I on H.病患檔_Counter=I.病患檔_counter and H.就診日=I.日期
                    where C.轉檢單位代號 in ('JY01180077','JY01180086','350102E285','3503272627')
                    and (C.開醫令日期時間 like @checkTime or C.異動日期時間 like @checkTime)";

            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<ChiehDto>(sql, new { checkTime = $"%{checkTime}%" });
        }
    }
}