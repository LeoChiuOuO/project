using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Controllers
{
    public class ReportPageController : Controller
    {
        private readonly IReportService _reportService;

        public ReportPageController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var reports = _reportService.GetAll();
            return View(reports);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Dele()
        {
            return View();
        }
    }
}