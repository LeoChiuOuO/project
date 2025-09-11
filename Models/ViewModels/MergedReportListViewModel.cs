namespace WebApplication_Dianthus.Models.ViewMosels;
// 列表顯示用的 ViewModel
public class MergedReportListViewModel
{
    public string SourceTable { get; set; }
    public string ReportId { get; set; }
    public string TestID { get; set; }
    public string CustName { get; set; }
    public string TestItem { get; set; }
    public DateTime TestingDate { get; set; }
    public DateTime ReportDate { get; set; }
    public string InspectionInstitution { get; set; }
    public string SendingPhysicianName { get; set; }
    public int Notification_Status { get; set; }
    public int Tracking_Status { get; set; }
}
