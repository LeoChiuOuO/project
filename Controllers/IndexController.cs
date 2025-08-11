using Microsoft.AspNetCore.Mvc;

namespace WebApplication_Dianthus.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index(int isLogin = 0)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Account, string Password)
        {
            if (Account == "admin" && Password == "admin")
            {
                return RedirectToAction("Welcome", null);
            }
            else
            {                                                                     
                ViewBag.errorMsg = "登入失敗";
                return View("~/Views/Index/Index.cshtml");
            }
        }

        public IActionResult Logout(int userID)
        {
            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }
    }
}