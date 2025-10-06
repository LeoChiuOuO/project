using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Dianthus.Models;
// 資料篩選條件
public class ReportFilter
{
    public List<int>? TestItemIds { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public List<string>? NotifyList { get; set; }
    public List<string>? TrackList { get; set; }
    public string? Keyword { get; set; }
    public List<string> NotifyStatus { get; set; } = new();
    public List<string> TrackStatus { get; set; } = new();

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // 權限過濾
    public string Role { get; set; } = "user";
    public int? PartitionId { get; set; }
    public int? DepartmentId { get; set; }

}
