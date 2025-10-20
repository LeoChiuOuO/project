namespace WebApplication_Dianthus.Models.Interface;
public interface IOperationLogRepository
{
    void Add(OperationLog log);
    IEnumerable<OperationLog> Query(string userId, string userName, string module, DateTime? from, DateTime? to);
}