using WebApplication_Dianthus.Models;

namespace WebApplication_Dianthus.Models.Service;

public class RolePermissionService : IRolePermissionService
{
    private readonly IRolePermissionRepository _rpRepo;
    public RolePermissionService(IRolePermissionRepository rpRepo) => _rpRepo = rpRepo;

    public IEnumerable<RolePermission> GetAllRolePermissions() => _rpRepo.GetAll();
    public RolePermission GetRolePermission(int id) => _rpRepo.GetById(id);
    public RolePermission GetRolePermissionByRoleId(int id) => _rpRepo.GetRolePermissionByRoleId(id);
    public void CreateRolePermission(RolePermission rolePermission) => _rpRepo.Add(rolePermission);
    public void UpdateRolePermission(int roleId, int permissionId, int partitionId, int departmentId, int operatorId)
    {
        _rpRepo.Update(roleId, permissionId, partitionId, departmentId, operatorId);
    }
    
    public void DeleteRolePermission(int id, int modifierId) => _rpRepo.Delete(id, modifierId);
}