namespace WebApplication_Dianthus.Models.Service.Interface
{
    public interface IReportService
    {
        PagedResult<Report> GetReports(ReportFilter filter);
        Report? GetReportById(int id);
        List<string> GetSpecimenTypeOptions(string columnName);
        byte[] ExportReports(ReportFilter filter);
        void CreateReport(Report report);
        bool UpdateReport(ReportUpdateDto report);
        void DeleteReport(int id);
    }
}