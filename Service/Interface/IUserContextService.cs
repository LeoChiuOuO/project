namespace WebApplication_Dianthus.Models.Service.Interface;

public interface IUserContextService
{
    int GetUserId();
    bool IsAdmin(int id);
    int? GetPartitionId();
    int? GetDepartmentId();
    string GetUserName();
}