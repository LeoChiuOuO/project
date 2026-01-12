namespace WebApplication_Dianthus.Models.Interface
{
    public interface ITrackingTimelineRepository
    {
        Task<TrackingTimeline> CreateAsync(TrackingTimeline entity);
        Task<TrackingTimeline?> UpdateAsync(TrackingTimeline entity);
        Task<TrackingTimeline?> GetByIdAsync(int id);
        Task<IEnumerable<TrackingTimeline>> GetByReportIdAsync(int reportId);
    }
}