using WebApplication_Dianthus.Models;

namespace WebApplication_Dianthus.Models.Service;

public class RolePermissionService : IRolePermissionService
{
    private readonly IRolePermissionRepository _rpRepo;
    public RolePermissionService(IRolePermissionRepository rpRepo) => _rpRepo = rpRepo;

    public IEnumerable<RolePermission> GetAllRolePermissions() => _rpRepo.GetAll();
    public RolePermission GetRolePermission(int id) => _rpRepo.GetById(id);
    public void CreateRolePermission(RolePermission rolePermission) => _rpRepo.Add(rolePermission);
    public void UpdateRolePermission(RolePermission rolePermission) => _rpRepo.Update(rolePermission);
    public void DeleteRolePermission(int id) => _rpRepo.Delete(id);
}