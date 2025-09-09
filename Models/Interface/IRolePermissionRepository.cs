using WebApplication_Dianthus.Models;

public interface IRolePermissionRepository
{
    IEnumerable<RolePermission> GetAll();
    RolePermission GetById(int id);
    void Add(RolePermission rolePermission);
    void Update(RolePermission rolePermission);
    void Delete(int id);
}