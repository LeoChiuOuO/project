using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Repository;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly AppDbContext _context;

    public UserRoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddUserRole(int userId, int roleId)
    {
        var entity = new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };
        _context.UserRoles.Add(entity);
        _context.SaveChanges();
    }

    public void UpdateUserRoles(int userId, int roleId)
    {
        var existing = _context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
        if (existing != null)
        {
            existing.RoleId = roleId;
            _context.SaveChanges();
        }
    }

    public void RemoveUserRole(int userId, int roleId)
    {
        var relation = _context.UserRoles
            .FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);

        if (relation != null)
        {
            _context.UserRoles.Remove(relation);
            _context.SaveChanges();
        }
    }

    public string GetRoleIdsByUserId(int userId)
    {
        var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
        if (userRole == null)
        {
            return null;
        }

        return userRole.RoleId.ToString();
    }

    public UserRole GetByUserId(int userId)
    {
        var result = _context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
        return result;
    }

    public void Delete(UserRole userRole)
    {
        _context.UserRoles.Remove(userRole);
        _context.SaveChanges();
    }
    public IEnumerable<UserRole> GetOtherUsersByRole(int roleId, int excludeUserId)
    {
        return _context.UserRoles
        .Where(ur => ur.RoleId == roleId && ur.UserId != excludeUserId && ur.DeletedAt == null)
        .ToList();
    }

}