namespace WebApplication_Dianthus.Models.Service.Interface;
public interface IOperationLogService
{
    void Log(string actionType, string module, bool success, string description);
    IEnumerable<OperationLog> Search(string userId, string userName, string module, DateTime? from, DateTime? to);
}