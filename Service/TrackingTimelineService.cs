using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Models.Service
{
    public class TrackingTimelineService : ITrackingTimelineService
    {
        private readonly ITrackingTimelineRepository _repo;

        public TrackingTimelineService(ITrackingTimelineRepository repo)
        {
            _repo = repo;
        }

        // 建立
        public async Task<TrackingTimeline> CreateAsync(TrackingTimeline entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            return await _repo.CreateAsync(entity);
        }

        // 更新
        public async Task<TrackingTimeline?> UpdateAsync(int id, TrackingTimeline entity)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = entity.Name;
            existing.Status = entity.Status;
            existing.Content = entity.Content;
            existing.TrackingDate = entity.TrackingDate;
            existing.UpdatedAt = DateTime.Now;

            return await _repo.UpdateAsync(existing);
        }

        // 依 ReportId 讀取 (同步版本可刪掉，建議只保留 async)
        public async Task<IEnumerable<TrackingTimeline>> GetByReportIdAsync(int reportId)
        {
            return await _repo.GetByReportIdAsync(reportId);
        }

        // 依 Id 讀取
        public async Task<TrackingTimeline?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public IEnumerable<TrackingTimeline> GetByReportId(int reportId)
        {
            throw new NotImplementedException();
        }
    }
}