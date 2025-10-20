using Microsoft.AspNetCore.Mvc;

namespace WebApplication_Dianthus.Controllers;
public class LogViewerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}