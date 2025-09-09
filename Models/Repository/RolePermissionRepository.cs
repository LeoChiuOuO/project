using WebApplication_Dianthus.Models;

namespace WebApplication_Dianthus.Models.Repository;

public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly AppDbContext _context;
    public RolePermissionRepository(AppDbContext context) => _context = context;

    public IEnumerable<RolePermission> GetAll() => _context.RolePermissions.ToList();
    public RolePermission GetById(int id) => _context.RolePermissions.Find(id);
    public void Add(RolePermission rolePermission) { _context.RolePermissions.Add(rolePermission); _context.SaveChanges(); }
    public void Update(RolePermission rolePermission) { _context.RolePermissions.Update(rolePermission); _context.SaveChanges(); }
    public void Delete(int id)
    {
        var rp = _context.RolePermissions.Find(id);
        if (rp != null)
        {
            _context.RolePermissions.Remove(rp);
            _context.SaveChanges();
        }
    }
}