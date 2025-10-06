using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.DTO;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserRepository _userRepo;

        public PermissionsController(IPermissionService permissionService, IUserRepository userRepo)
        {
            _permissionService = permissionService;
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
            var list = _permissionService.GetAllPermissions();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin()) return Forbid();
            return PartialView("Create", new Permission());
        }

        [HttpPost]
        public IActionResult Create(Permission permission)
        {
            if (!IsAdmin()) return Forbid();

            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            permission.CreateId = int.Parse(userId);
            permission.CreatedAt = DateTime.Now;
            permission.UpdatedAt = DateTime.Now;

            _permissionService.CreatePermission(permission);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult UpdateAll(PermissionDTO dto)
        {
            if (!IsAdmin()) return Forbid();

            var permission = _permissionService.GetPermission(dto.Id);
            if (permission == null) return Json(new { success = false, message = "找不到資料" });

            permission.ReviewPermissions = dto.ReviewPermissions;
            permission.CreatePermissions = dto.CreatePermissions;
            permission.EditPermissions = dto.EditPermissions;
            permission.DeletePermissions = dto.DeletePermissions;

            permission.UpdatedAt = DateTime.Now;
            var userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId)) permission.ModifyId = int.Parse(userId);

            _permissionService.UpdatePermission(permission);
            return Json(new { success = true });
        }


        [HttpPost]
        public IActionResult UpdateField(int id, string field, bool value)
        {
            if (!IsAdmin()) return Forbid();

            var permission = _permissionService.GetPermission(id);
            if (permission == null) return Json(new { success = false, message = "找不到資料" });

            switch (field)
            {
                case "ReviewPermissions": permission.ReviewPermissions = value; break;
                case "CreatePermissions": permission.CreatePermissions = value; break;
                case "EditPermissions": permission.EditPermissions = value; break;
                case "DeletePermissions": permission.DeletePermissions = value; break;
                default: return Json(new { success = false, message = "欄位無效" });
            }

            permission.UpdatedAt = DateTime.Now;
            var userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId)) permission.ModifyId = int.Parse(userId);

            _permissionService.UpdatePermission(permission);
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAdmin()) return Forbid();
            var perm = _permissionService.GetPermission(id);
            if (perm == null) return NotFound();
            return PartialView("Edit", perm);
        }

        [HttpPost]
        public IActionResult Edit(Permission permission)
        {
            if (!IsAdmin()) return Forbid();

            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            permission.ModifyId = int.Parse(userId);
            permission.UpdatedAt = DateTime.Now;

            _permissionService.UpdatePermission(permission);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return Forbid();
            _permissionService.DeletePermission(id);
            return Json(new { success = true });
        }
    }
}