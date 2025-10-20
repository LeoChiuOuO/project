using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Models.Service;

public class OperationLogService : IOperationLogService
{
    private readonly IOperationLogRepository _repo;
    private readonly IHttpContextAccessor _http;

    public OperationLogService(IOperationLogRepository repo, IHttpContextAccessor http)
    {
        _repo = repo;
        _http = http;
    }

    public void Log(string actionType, string module, bool success, string description)
    {
        var session = _http.HttpContext?.Session;
        var userIdStr = session?.GetString("UserId");
        var userId = int.TryParse(userIdStr, out var parsedId) ? parsedId : -1;
        var userName = session?.GetString("UserName") ?? "Unknown";
        var ip = _http.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "0.0.0.0";

        var log = new OperationLog
        {
            UserId = userId,
            UserName = userName,
            IpAddress = ip,
            ActionType = actionType,
            Module = module,
            Success = success,
            Description = description,
            CreatedAt = DateTime.Now
        };

        _repo.Add(log);
    }

    public IEnumerable<OperationLog> Search(string userId, string userName, string module, DateTime? from, DateTime? to)
    {
        return _repo.Query(userId, userName, module, from, to);
    }
}