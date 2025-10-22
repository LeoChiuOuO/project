using WebApplication_Dianthus.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication_Dianthus.Models.Repository;

public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly AppDbContext _context;
    public RolePermissionRepository(AppDbContext context) => _context = context;

    public IEnumerable<RolePermission> GetAll() => _context.RolePermissions.ToList();
    public RolePermission GetById(int id) => _context.RolePermissions.Find(id);

    public int GetPartitionIdsByRoleIds(int id) 
    {
        var result = _context.RolePermissions.FirstOrDefault(r => r.RoleId == id);
        return result.PartitionId;
    }
    public int GetDepartmentIdsByRoleIds(int id)
    {
        var result = _context.RolePermissions.FirstOrDefault(r => r.RoleId == id);
        return result.DepartmentId;
    }

    public List<RolePermission> GetByRoleId(int roleId)
    {
        return _context.RolePermissions
            .Where(rp => rp.RoleId == roleId && rp.DeletedAt == null)
            .ToList();
    }


    public void Add(RolePermission rolePermission)
    {
        rolePermission.CreatedAt = DateTime.Now;
        rolePermission.UpdatedAt = DateTime.Now;
        _context.RolePermissions.Add(rolePermission);
        _context.SaveChanges();
    }

    public void Update(int roleId, int permissionId, int partitionId, int departmentId, int operatorId)
    {
        var existing = _context.RolePermissions.FirstOrDefault(rp => rp.RoleId == roleId);
        if (existing != null)
        {
            existing.PermissionsId = permissionId;
            existing.PartitionId = partitionId;
            existing.DepartmentId = departmentId;
            existing.ModifyId = operatorId;
            existing.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
        }

    }
    
    public void Delete(int id, int modifierId)
    {
        var entity = _context.RolePermissions.FirstOrDefault(rp => rp.Id == id);
        if (entity == null) throw new Exception("資料不存在");

        entity.DeletedAt = DateTime.Now;
        entity.ModifyId = modifierId;
        entity.UpdatedAt = DateTime.Now;

        _context.SaveChanges();
    }

    public RolePermission GetRolePermissionByRoleId(int roleId)
    {
        var result = _context.RolePermissions
                    .Include(rp => rp.Partition)
                    .Include(rp => rp.Department)
                    .Include(rp => rp.Permission)
                    .AsNoTracking()
                    .FirstOrDefault(rp => rp.RoleId == roleId);
        return result;
    }

    public void DeleteByRoleId(int roleId)
    {
        var rolePermissions = _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .ToList();

        _context.RolePermissions.RemoveRange(rolePermissions);
        _context.SaveChanges();
    }
}