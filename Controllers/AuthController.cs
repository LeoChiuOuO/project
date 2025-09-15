using Microsoft.AspNetCore.Mvc;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public JsonResult Login(string account, string password)
    {
        try
        {
            if (_authService.ValidateUser(account, password, out var user))
            {
                _authService.UpdateLastLoginDate(user);
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return Json(new { success = true, message = "登入成功" });
            }

            return Json(new { success = false, message = "帳號或密碼錯誤" });
        }
        catch (Exception ex)
        {
            Console.WriteLine("登入錯誤：" + ex.Message);
            return Json(new { success = false, message = "伺服器錯誤：" + ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear(); //清除所有Session
        return RedirectToAction("Login", "Auth"); //倒回登入頁
    }
}
