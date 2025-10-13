using WebApplication_Dianthus.Models.DTO;

namespace WebApplication_Dianthus.Models.Service.Interface
{
    public interface IReportService
    {
        PagedResult<Report> GetReports(ReportFilter filter);
        PagedResult<ReportDTO> SearchReports(ReportFilter filter);

        Report? GetReportById(int id);
        List<string> GetSpecimenTypeOptions(string columnName);
        byte[] ExportReports(ReportFilter filter);
        byte[] ExportSimplifiedReports(ReportFilter filter);
        byte[] ExportFullReports(ReportFilter filter);
        List<TestItem> GetAllTestItem();
        void CreateReport(Report report);
        bool UpdateReport(ReportUpdateDto report);
        void DeleteReport(int id);
    }
}