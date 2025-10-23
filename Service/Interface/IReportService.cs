using WebApplication_Dianthus.Models.DTO;

namespace WebApplication_Dianthus.Models.Service.Interface
{
    public interface IReportService
    {
        PagedResult<Report> GetReports(ReportFilter filter);
        PagedResult<ReportDTO> SearchReports(ReportFilter filter, string partitionId, bool isAdmin);

        Report? GetReportById(int id);
        List<string> GetSpecimenTypeOptions(string columnName);
        byte[] ExportSimplifiedReports(ReportFilter filter);
        byte[] ExportFullReports(ReportFilter filter);
        List<TestItem> GetAllTestItem();
        void CreateReport(Report report);
        bool UpdateReport(ReportUpdateDTO report);
        void DeleteReport(int id);
        ReportDashboardViewModel GetDashboard();

    }
}