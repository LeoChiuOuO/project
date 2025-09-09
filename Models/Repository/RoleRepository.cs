using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Interface;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;
    public RoleRepository(AppDbContext context) => _context = context;

    public IEnumerable<Role> GetAll() => _context.Roles.ToList();
    public Role GetById(int id) => _context.Roles.Find(id);
    public void Add(Role role) { _context.Roles.Add(role); _context.SaveChanges(); }
    public void Update(Role role) { _context.Roles.Update(role); _context.SaveChanges(); }
    public void Delete(int id)
    {
        var role = _context.Roles.Find(id);
        if (role != null)
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
        }
    }
}