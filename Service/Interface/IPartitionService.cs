namespace WebApplication_Dianthus.Models.Service.Interface;
public interface IPartitionService
{
    List<Partition> GetAll();
    Partition GatById(int id);
}