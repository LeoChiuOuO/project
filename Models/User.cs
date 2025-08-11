
namespace WebApplication_Dianthus.Models;

public class User
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Account { get; set; }
    public string Password { get; set; }
    public string Department { get; set; }
    public Guid RoleID { get; set; }
}
