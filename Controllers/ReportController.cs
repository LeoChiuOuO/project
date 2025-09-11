using Microsoft.AspNetCore.Mvc;

namespace WebApplication_Dianthus.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}