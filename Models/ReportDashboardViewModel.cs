namespace WebApplication_Dianthus.Models;
public class ReportDashboardViewModel
{
    public int UnreadCount { get; set; }
    public int UpcomingOverdueCount { get; set; }
    public int OverdueCount { get; set; }
    public int CriticalOverdueCount { get; set; }
    public int CriticalAssessmentCount { get; set; }
}
