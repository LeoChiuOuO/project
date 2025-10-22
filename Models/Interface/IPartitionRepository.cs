namespace WebApplication_Dianthus.Models.Interface;
public interface IPartitionRepository
{
    List<Partition> GetAll();
    Partition GetById(int id);
}