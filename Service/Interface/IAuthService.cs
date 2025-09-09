using DocumentFormat.OpenXml.Spreadsheet;
using WebApplication_Dianthus.Models;

public interface IAuthService
{
    bool ValidateUser(string account, string password, out User user);
    void UpdateLastLoginDate(User user);
}