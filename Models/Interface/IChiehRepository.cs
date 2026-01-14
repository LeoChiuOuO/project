namespace WebApplication_Dianthus.Models.Interface
{
    public interface IChiehRepository
    {
        Task<IEnumerable<ChiehDto>> GetDataAsync(string checkTime);
    }
}