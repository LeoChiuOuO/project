using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.DTO;
using WebApplication_Dianthus.Models.Service.Interface;

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

            var reportFilter = BuildFilterFromType(filter);
            reportFilter.PartitionId = int.Parse(partitionId);
            var pagedReports = _report.SearchReports(reportFilter, partitionId, isAdmin);
            var testItems = _report.GetAllTestItem();

            var vm = new ReportIndexViewModel
            {
                Reports = pagedReports,
                TestItems = testItems,
                FilterType = filter,
                PartitionId = int.Parse(partitionId)
            };

            return View(vm);
        }

        // Controllers/ReportController.cs
        [HttpGet("/Report/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            // 取得 Session 值
            var currentUserName = HttpContext.Session.GetString("CurrentUserName");
            var userId = int.Parse(HttpContext?.Session.GetString("UserId"));

            // 把 Session 值傳到 View
            ViewBag.CurrentUserName = currentUserName;
            ViewBag.CurrentUserID = userId;
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

        private ReportFilter BuildFilterFromType(string type)
        {
            var today = DateTime.Today;
            var filter = new ReportFilter
            {
                Page = 1,
                PageSize = 100
            };

            switch (type)
            {
                case "unread":
                    filter.NotifyStatus = new List<string> { "待通知" };
                    filter.TrackStatus = new List<string> { "待追蹤" };
                    break;
                case "upcoming":
                    filter.NotifyStatus = new List<string> { "待通知" };
                    filter.TrackStatus = new List<string> { "待追蹤" };
                    filter.DateFrom = today.AddDays(-30);
                    filter.DateTo = today.AddDays(-20);
                    break;
                case "overdue":
                    filter.NotifyStatus = new List<string> { "待通知" };
                    filter.TrackStatus = new List<string> { "待追蹤" };
                    filter.DateFrom = today.AddDays(-60);
                    filter.DateTo = today.AddDays(-30);
                    break;
                case "critical":
                    filter.NotifyStatus = new List<string> { "待通知" };
                    filter.TrackStatus = new List<string> { "待追蹤" };
                    filter.DateFrom = today.AddDays(-365);
                    filter.DateTo = today.AddDays(-60);
                    break;
                case "assessment":
                    filter.NotifyStatus = new List<string> { "待通知" };
                    filter.TrackStatus = new List<string> { "待追蹤" };
                    filter.AssessmentStatus = "重大"; // 或你可以加 AssessmentStatus 條件
                    filter.DateFrom = today.AddDays(-365);
                    break;
            }

            return filter;
        }
    }
}