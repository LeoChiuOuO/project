using WebApplication_Dianthus.Models;

public interface IRolePermissionService
{
    IEnumerable<RolePermission> GetAllRolePermissions();
    RolePermission GetRolePermission(int id);
    void CreateRolePermission(RolePermission rolePermission);
    void UpdateRolePermission(RolePermission rolePermission);
    void DeleteRolePermission(int id);
}