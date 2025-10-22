namespace WebApplication_Dianthus.Models.Interface;
public interface IDepartmentRepository
{
    List<Department> GetAll();
    Department GetById(int id);
    List<Department> GetByPartitionId(int partitionId);
}