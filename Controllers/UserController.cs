namespace WebApplication_Dianthus.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebApplication_Dianthus.Models;
    using WebApplication_Dianthus.Models.Interface;
    using WebApplication_Dianthus.Models.Service.Interface;
    using Microsoft.AspNetCore.Http;

    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepo;

        public UserController(IUserService userService, IUserRepository userRepo)
        {
            _userService = userService;
            _userRepo = userRepo;
        }

        private bool IsAdmin()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return false;
            var user = _userRepo.GetById(int.Parse(userId));
            return user != null && user.Account == "admin";
        }


        // 個人資料功能（登入者自己）
        [HttpGet]
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Auth");

            var user = _userService.GetById(int.Parse(userId));
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        public IActionResult Profile(User model)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Auth");

            // 確保只能改自己的資料
            model.Id = int.Parse(userId);
            try
            {
                _userService.UpdateUser(model);
                TempData["Message"] = "基本資料已更新";
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Profile");
        }

        // 管理員功能（帳號管理）
        public IActionResult Index()
        {
            if (!IsAdmin()) return Forbid();
            var list = _userService.GetAllUsers();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin()) return Forbid();
            return PartialView("Create", new User());
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (!IsAdmin()) return Forbid();
            _userService.CreateUser(user);
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAdmin()) return Forbid();
            var u = _userService.GetById(id);
            if (u == null) return NotFound();
            return PartialView("Edit", u);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (!IsAdmin()) return Forbid();
            _userService.UpdateUser(user);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return Forbid();
            _userService.DeleteUser(id);
            return Json(new { success = true });
        }
    }
}