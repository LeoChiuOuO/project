using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IUserRepository _userRepo;

        public UserRolesController(IUserRoleService userRoleService, IUserRepository userRepo)
        {
            _userRoleService = userRoleService;
            _userRepo = userRepo;
        }

        private bool IsAdmin()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return false;
            var user = _userRepo.GetById(int.Parse(userId));
            return user != null && user.Account == "admin";
        }

        public IActionResult Index()
        {
            if (!IsAdmin()) return Forbid();
            var list = _userRoleService.GetAll();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin()) return Forbid();
            return PartialView("Create", new UserRole());
        }

        [HttpPost]
        public IActionResult Create(UserRole ur)
        {
            if (!IsAdmin()) return Forbid();
            _userRoleService.CreateUserRole(ur);
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAdmin()) return Forbid();
            var ur = _userRoleService.GetByUserId(id);
            if (ur == null) return NotFound();
            return PartialView("Edit", ur);
        }

        [HttpPost]
        public IActionResult Edit(UserRole ur)
        {
            if (!IsAdmin()) return Forbid();
            _userRoleService.UpdateUserRole(ur);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return Forbid();
            _userRoleService.DeleteUserRole(id);
            return Json(new { success = true });
        }
    }
}