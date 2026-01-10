namespace WebApplication_Dianthus.Models.Service.Interface
{
    public interface IConsultRecordService
    {
        Task<IEnumerable<ConsultRecord>> GetRecordsAsync(int reportId);
        Task<ConsultRecord?> GetRecordAsync(int id);
        Task<int> CreateRecordAsync(int reportId,int currentUserId, string name, string content);
        Task UpdateRecordAsync(int id, string content);
        Task DeleteRecordAsync(int id);
    }
}