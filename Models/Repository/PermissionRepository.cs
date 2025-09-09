using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Repository;
public class PermissionRepository : IPermissionRepository
{
    private readonly AppDbContext _context;
    public PermissionRepository(AppDbContext context) => _context = context;

    public IEnumerable<Permission> GetAll() => _context.Permissions.ToList();
    public Permission GetById(int id) => _context.Permissions.Find(id);
    public void Add(Permission permission) { _context.Permissions.Add(permission); _context.SaveChanges(); }
    public void Update(Permission permission) { _context.Permissions.Update(permission); _context.SaveChanges(); }
    public void Delete(int id)
    {
        var permission = _context.Permissions.Find(id);
        if (permission != null)
        {
            _context.Permissions.Remove(permission);
            _context.SaveChanges();
        }
    }
}