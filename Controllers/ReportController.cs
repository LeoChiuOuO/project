using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models.Service.Interface;
using WebApplication_Dianthus.Services;

namespace WebApplication_Dianthus.Controllers
{
    public class ReportController : Controller
    {
        private readonly IUserService _userService;
        private readonly IReportService _report;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IPermissionService _permissionService;
        public ReportController(IReportService report,
                                IUserService userService,
                                IRolePermissionService rolePermissionService,
                                IPermissionService permissionService)
        {
            _report = report;
            _userService = userService;
            _rolePermissionService = rolePermissionService;
            _permissionService = permissionService;
        }
        public IActionResult Index()
        {
            var testitems = _report.GetAllTestItem();
            return View(testitems);
        }

        // Controllers/ReportController.cs
        [HttpGet("/Report/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var report = _report.GetReportById(id);
            return View(report);
        }

        [HttpGet("/api/auth/permission")]
        public IActionResult GetPermissionStatus()
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var roleId = _userService.GetRoleIdByUserId(userId);
            var result = _rolePermissionService.GetRolePermissionByRoleId(int.Parse(roleId));
            var permission = _permissionService.GetPermission(result.PermissionsId);

            return Json(new {
                canRead = permission?.ReviewPermissions ?? false,
                canUpdate = permission?.EditPermissions ?? false
            });
        }
    }
}