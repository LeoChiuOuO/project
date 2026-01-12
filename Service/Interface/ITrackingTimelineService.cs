namespace WebApplication_Dianthus.Models.Service.Interface
{
    public interface ITrackingTimelineService
    {

        Task<TrackingTimeline> CreateAsync(TrackingTimeline entity);
        Task<TrackingTimeline?> UpdateAsync(int id, TrackingTimeline entity);
        IEnumerable<TrackingTimeline> GetByReportId(int reportId);
        Task<IEnumerable<TrackingTimeline>> GetByReportIdAsync(int reportId);
        Task<TrackingTimeline?> GetByIdAsync(int id);
    }
}