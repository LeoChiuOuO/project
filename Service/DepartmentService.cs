using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Models.Service;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepo;
    public DepartmentService(IDepartmentRepository departmentRepo)
    {
        _departmentRepo = departmentRepo;
    }

    public List<Department> GetAll()
    {
        return _departmentRepo.GetAll();
    }

    public Department GetById(int id)
    {
        throw new NotImplementedException();
    }

    public List<Department> GetByPartitionId(int partitionId)
    {
        return _departmentRepo.GetByPartitionId(partitionId);
    }
}