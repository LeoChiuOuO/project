using WebApplication_Dianthus.Models.ViewMosels;

namespace WebApplication_Dianthus.Models.Service.Interface
{
    public interface IReportService
    {
        PagedResult<Report> GetReports(ReportFilter filter);
        Report? GetReportById(int id);
        List<string> GetSpecimenTypeOptions(string columnName);
        void CreateReport(Report report);
        bool UpdateReport(ReportUpdateDto report);
        void DeleteReport(int id);
    }
}