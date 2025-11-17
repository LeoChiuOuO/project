using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.DTO;
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
        private readonly IUserContextService _userContext;

        public ReportController(IReportService report,
                                IUserService userService,
                                IRolePermissionService rolePermissionService,
                                IPermissionService permissionService,
                                IUserContextService userContext)
        {
            _report = report;
            _userService = userService;
            _rolePermissionService = rolePermissionService;
            _permissionService = permissionService;
            _userContext = userContext;
        }
        public IActionResult Index(string? filter)
        {
            var partitionId = HttpContext.Session.GetString("PartitionId");
            var userId = int.Parse(HttpContext?.Session.GetString("UserId"));
            var isAdmin = _userContext.IsAdmin(userId);

            var reportFilter = _report.BuildFilterFromType(filter);
            reportFilter.PartitionId = int.Parse(partitionId);
            var pagedReports = _report.SearchReports(reportFilter, partitionId, isAdmin);
            var testItems = _report.GetAllTestItem();

            var vm = new ReportIndexViewModel
            {
                Reports = pagedReports,
                TestItems = testItems,
                FilterType = filter
            };

            return View(vm);
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