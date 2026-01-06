namespace WebApplication_Dianthus.Models.Interface
{
    public interface IConsultRecordRepository
    {
        Task<IEnumerable<ConsultRecord>> GetByReportIdAsync(int reportId);
        Task<ConsultRecord?> GetByIdAsync(int id);
        Task<int> AddAsync(ConsultRecord record);
        Task UpdateAsync(ConsultRecord record);
        Task DeleteAsync(int id);
    }
}
