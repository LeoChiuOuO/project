using WebApplication_Dianthus.Models;

public interface IRolePermissionService
{
    IEnumerable<RolePermission> GetAllRolePermissions();
    RolePermission GetRolePermission(int id);
    RolePermission GetRolePermissionByRoleId(int RoleId);
    void CreateRolePermission(RolePermission rolePermission);
    void UpdateRolePermission(int roleId, int permissionId, int partitionId, int departmentId, int operatorId);
    void DeleteRolePermission(int id,int modifierId);
}