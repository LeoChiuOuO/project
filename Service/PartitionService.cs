using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Models.Service;

public class PartitionService : IPartitionService
{
    private readonly IPartitionRepository _partitionRepo;
    public PartitionService(IPartitionRepository partitionRepo)
    {
        _partitionRepo = partitionRepo;
    }
    
    public List<Partition> GetAll()
    {
        return _partitionRepo.GetAll();
    }

    public Partition GatById(int id)
    {
        throw new NotImplementedException();
    }
}