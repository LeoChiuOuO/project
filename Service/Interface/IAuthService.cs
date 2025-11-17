using DocumentFormat.OpenXml.Spreadsheet;
using WebApplication_Dianthus.Models;

public interface IAuthService
{
    User GetCurrentUser();
    bool ValidateUser(string account, string password, out User user);
    void UpdateLastLoginDate(User user);
    (int? PartitionId, int? DepartmentId) GetDataScope(string permissionName);
    UserContext GetUserContext();
    bool IsAdmin(int roleId);
}