using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Service
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public List<Report> GetAll()
        {
            var reports = _reportRepository.GetAll();
            return reports;
        }
    }
}