using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Repository;

public class PartitionRepository : IPartitionRepository
{
    private readonly AppDbContext _context;
    public PartitionRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Partition> GetAll()
    {
        var result = _context.Partitions.ToList();
        return result;
    }

    public Partition GetById(int id)
    {
        var result = _context.Partitions.Find(id);
        return result;
    }
}