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
                return Json(new { success = true });
            }
            catch (ArgumentException ex)
            {
                return Json(new { success = false, ErrorMessage = ex.Message });
            }
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
            return View(new User());
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (!IsAdmin()) return Forbid();
            try
            {
                _userService.CreateUser(user);
                return Json(new { success = true });
            }
            catch (ArgumentException ex)
            {
                return Json(new { success = false, ErrorMessage = ex.Message});
            }
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
            try
            {
                _userService.UpdateUser(user);
                return Json(new { success = true });
            }
            catch (ArgumentException ex)
            {
                return Json(new { success = false, ErrorMessage = ex.Message});
            }

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return Json(new { success = false, message = "沒有權限" });
            try
            {
                _userService.DeleteUser(id); //軟刪除
                return Json(new { success = true});
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ErrorMessage = ex.Message});
            }

        }
    }
}