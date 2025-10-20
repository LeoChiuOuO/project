using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models.Service.Interface;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IOperationLogService _log;

    public AuthController(IAuthService authService, IOperationLogService log)
    {
        _authService = authService;
        _log = log;
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
                // 判斷是否已軟刪除
                if (user.DeletedAt != null)
                {
                    return Json(new { success = false, message = "此帳號已停用，請聯絡管理員" });
                }

                _authService.UpdateLastLoginDate(user);
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Name);
                _log.Log(actionType: "Login", module: "Auth", success: true, description: "使用者登入成功");

                return Json(new { success = true, message = "登入成功" });
            }
            
            _log.Log(actionType: "Login",module: "Auth",success: false,description: "帳號或密碼錯誤");
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
