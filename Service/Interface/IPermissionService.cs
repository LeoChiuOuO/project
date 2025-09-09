using WebApplication_Dianthus.Models;

public interface IPermissionService
{
    IEnumerable<Permission> GetAllPermissions();
    Permission GetPermission(int id);
    void CreatePermission(Permission permission);
    void UpdatePermission(Permission permission);
    void DeletePermission(int id);
}