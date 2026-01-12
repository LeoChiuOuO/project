using Microsoft.EntityFrameworkCore;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Repository
{
    public class TrackingTimelineRepository : ITrackingTimelineRepository
    {
        private readonly AppDbContext _context;

        public TrackingTimelineRepository(AppDbContext context)
        {
            _context = context;
        }

        // 建立
        public async Task<TrackingTimeline> CreateAsync(TrackingTimeline entity)
        {
            _context.TrackingTimelines.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // 更新
        public async Task<TrackingTimeline?> UpdateAsync(TrackingTimeline entity)
        {
            _context.TrackingTimelines.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // 依 Id 讀取
        public async Task<TrackingTimeline?> GetByIdAsync(int id)
        {
            return await _context.TrackingTimelines
                .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);
        }

        // 依 ReportId 讀取
        public async Task<IEnumerable<TrackingTimeline>> GetByReportIdAsync(int reportId)
        {
            return await _context.TrackingTimelines
                .Where(x => x.ReportId == reportId && x.DeletedAt == null)
                .ToListAsync();
        }
    }
}