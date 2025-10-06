using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models.Service.Interface;
using WebApplication_Dianthus.Services;

namespace WebApplication_Dianthus.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _report;
        public ReportController(IReportService report)
        {
            _report = report;
        }
        public IActionResult Index()
        {
            var testitems = _report.GetAllTestItem();
            return View(testitems);
        }

        // Controllers/ReportController.cs
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var report = _report.GetReportById(id);
            return View(report);
        }
    }
}