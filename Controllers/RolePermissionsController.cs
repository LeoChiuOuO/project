using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Controllers
{
    public class RolePermissionsController : Controller
    {
        private readonly IRolePermissionService _rpService;
        private readonly IUserRepository _userRepo;

        public RolePermissionsController(IRolePermissionService rpService, IUserRepository userRepo)
        {
            _rpService = rpService;
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
            var list = _rpService.GetAllRolePermissions();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin()) return Forbid();
            return PartialView("Create", new RolePermission());
        }

        [HttpPost]
        public IActionResult Create(RolePermission rp)
        {
            if (!IsAdmin()) return Forbid();
            _rpService.CreateRolePermission(rp);
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAdmin()) return Forbid();
            var rp = _rpService.GetRolePermission(id);
            if (rp == null) return NotFound();
            return PartialView("Edit", rp);
        }

        [HttpPost]
        public IActionResult Edit(RolePermission rp)
        {
            if (!IsAdmin()) return Forbid();
            _rpService.UpdateRolePermission(rp);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return Forbid();
            _rpService.DeleteRolePermission(id);
            return Json(new { success = true });
        }
    }
}