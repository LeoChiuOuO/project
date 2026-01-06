using Microsoft.EntityFrameworkCore;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Repository
{
    public class ConsultRecordRepository : IConsultRecordRepository
    {
        private readonly AppDbContext _context;

        public ConsultRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConsultRecord>> GetByReportIdAsync(int reportId)
            => await _context.ConsultRecords
                            .Where(r => r.ReportId == reportId && r.DeletedAt == null)
                            .ToListAsync();

        public async Task<ConsultRecord?> GetByIdAsync(int id)
            => await _context.ConsultRecords.FindAsync(id);

        public async Task<int> AddAsync(ConsultRecord record)
        {
            _context.ConsultRecords.Add(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task UpdateAsync(ConsultRecord record)
        {
            _context.ConsultRecords.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _context.ConsultRecords.FindAsync(id);
            if (record != null)
            {
                record.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}