using Microsoft.AspNetCore.Mvc;

namespace WebApplication_Dianthus.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}