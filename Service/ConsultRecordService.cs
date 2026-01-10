
using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Models.Service
{
    public class ConsultRecordService : IConsultRecordService
    {
        private readonly IConsultRecordRepository _repository;

        public ConsultRecordService(IConsultRecordRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ConsultRecord>> GetRecordsAsync(int reportId)
            => await _repository.GetByReportIdAsync(reportId);

        public async Task<ConsultRecord?> GetRecordAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<int> CreateRecordAsync(int reportId, int currentUserId, string name, string content)
        {
            var record = new ConsultRecord
            {
                ReportId = reportId,
                CurrentUserId = currentUserId,
                Name = name,
                Content = content,
                CreatedAt = DateTime.Now
            };
            return await _repository.AddAsync(record);
        }

        public async Task UpdateRecordAsync(int id, string content)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record != null)
            {
                record.Content = content;
                record.UpdatedAt = DateTime.Now;
                await _repository.UpdateAsync(record);
            }
        }

        public async Task DeleteRecordAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}