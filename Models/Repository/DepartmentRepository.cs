using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Repository;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _context;
    public DepartmentRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Department> GetAll()
    {
        var result = _context.Departments.ToList();
        return result;
    }

    public Department GetById(int id)
    {
        var result = _context.Departments.Find(id);
        return result;
    }

    public List<Department> GetByPartitionId(int partitionId)
    {
        return _context.Departments
            .Where(d => d.PartitionId == partitionId && d.DeletedAt == null)
            .OrderBy(d => d.Name)
            .ToList();
    }
}