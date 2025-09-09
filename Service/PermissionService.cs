using WebApplication_Dianthus.Models.Interface;
namespace WebApplication_Dianthus.Models;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permRepo;
    public PermissionService(IPermissionRepository permRepo) => _permRepo = permRepo;

    public IEnumerable<Permission> GetAllPermissions() => _permRepo.GetAll();
    public Permission GetPermission(int id) => _permRepo.GetById(id);
    public void CreatePermission(Permission permission) => _permRepo.Add(permission);
    public void UpdatePermission(Permission permission) => _permRepo.Update(permission);
    public void DeletePermission(int id) => _permRepo.Delete(id);
}