using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Dianthus.Models
{
    public class TrackingTimeline
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("report_id")]
        public int ReportId { get; set; }

        [Column("current_user_id")]
        public int CurrentUserId { get; set; } = 0;

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("status")]
        public string Status { get; set; } = string.Empty;

        [Column("content")]
        public string Content { get; set; }

        [Column("tracking_date")]
        public DateTime TrackingDate {get; set;}

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        // 關聯到 Report
        public Report Report { get; set; }
    }
}