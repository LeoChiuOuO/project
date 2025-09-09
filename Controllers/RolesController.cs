using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Controllers
{
   public class RolesController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserRepository _userRepo;

        public RolesController(IRoleService roleService, IUserRepository userRepo)
        {
            _roleService = roleService;
            _userRepo = userRepo;
        }

        private bool IsAdmin()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var user = _userRepo.GetById(int.Parse(userId));
            return user != null && user.Account == "admin";
        }

        public IActionResult Index()
        {
            if (!IsAdmin()) return Forbid();
            return View(_roleService.GetAllRoles());
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin()) return Forbid();
            return PartialView("Create", new Role());
        }

        [HttpPost]
        public IActionResult Create(Role role)
        {
            if (!IsAdmin()) return Forbid();
            _roleService.CreateRole(role);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAdmin()) return Forbid();
            var role = _roleService.GetRole(id);
            if (role == null) return NotFound();
            return PartialView("Edit", role);
        }

        [HttpPost]
        public IActionResult Edit(Role role)
        {
            if (!IsAdmin()) return Forbid();
            _roleService.UpdateRole(role);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return Forbid();
            _roleService.DeleteRole(id);
            return RedirectToAction(nameof(Index));
        }
    }
}