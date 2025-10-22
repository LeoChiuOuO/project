namespace WebApplication_Dianthus.Models.Service.Interface;
public interface IDepartmentService
{
    List<Department> GetAll();
    Department GetById(int id);
    List<Department> GetByPartitionId(int partitionId);
}