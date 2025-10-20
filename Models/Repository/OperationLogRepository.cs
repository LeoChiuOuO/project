using System.Data;
using System.Text;
using Dapper;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Repository;

public class OperationLogRepository : IOperationLogRepository
{
    private readonly IDbConnection _db;

    public OperationLogRepository(IDbConnection db)
    {
        _db = db;
    }

    public void Add(OperationLog log)
    {
        string sql = @"INSERT INTO OperationLog 
            (user_id, user_name, ip_address, action_type, module, success, description, created_at)
            VALUES (@UserId, @UserName, @IpAddress, @ActionType, @Module, @Success, @Description, @CreatedAt)";
        _db.Execute(sql, log);
    }

    public IEnumerable<OperationLog> Query(string userId, string userName, string module, DateTime? from, DateTime? to)
    {
        var sql = new StringBuilder(@"
        SELECT 
            id, user_id AS UserId, user_name AS UserName, ip_address AS IpAddress, action_type AS ActionType, module AS Module, success AS Success, description AS Description, created_at AS CreatedAt 
        FROM OperationLog WHERE 1=1 ");

        var param = new DynamicParameters();

        if (!string.IsNullOrEmpty(userId))
        {
            sql.Append("AND user_id = @UserId ");
            param.Add("UserId", userId);
        }

        if (!string.IsNullOrEmpty(userName))
        {
            sql.Append("AND user_name LIKE @UserName ");
            param.Add("UserName", $"%{userName}%");
        }

        if (!string.IsNullOrEmpty(module))
        {
            sql.Append("AND module = @Module ");
            param.Add("Module", module);
        }

        if (from.HasValue)
        {
            sql.Append("AND created_at >= @From ");
            param.Add("From", from.Value);
        }

        if (to.HasValue)
        {
            sql.Append("AND created_at <= @To ");
            param.Add("To", to.Value);
        }

        sql.Append("ORDER BY created_at DESC");
        return _db.Query<OperationLog>(sql.ToString(), param);
    }
}