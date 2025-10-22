using WebApplication_Dianthus.Models;

public interface IRolePermissionRepository
{
    IEnumerable<RolePermission> GetAll();
    RolePermission GetById(int id);
    RolePermission GetRolePermissionByRoleId(int roleId);
    int GetPartitionIdsByRoleIds(int id);
    int GetDepartmentIdsByRoleIds(int id);
    List<RolePermission> GetByRoleId(int roleId);
    void Add(RolePermission rolePermission);
    void Update(int roleId, int permissionId, int partitionId, int departmentId, int operatorId);
    void Delete(int id, int modifierId);
    void DeleteByRoleId(int roleId);
}