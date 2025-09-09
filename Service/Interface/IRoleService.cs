using WebApplication_Dianthus.Models;

public interface IRoleService
{
    IEnumerable<Role> GetAllRoles();
    Role GetRole(int id);
    void CreateRole(Role role);
    void UpdateRole(Role role);
    void DeleteRole(int id);
}