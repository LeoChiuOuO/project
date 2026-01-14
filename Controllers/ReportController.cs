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
            filter.NotifyStatus = new List<string> { "待通知" };
            filter.TrackStatus = new List<string> { "待追蹤" };

            switch (type)
            {
                case "unread":
                    break;
                case "upcoming":
                    filter.DateFrom = today.AddDays(-30);
                    filter.DateTo = today.AddDays(-20);
                    break;
                case "overdue":
                    filter.DateFrom = today.AddDays(-60);
                    filter.DateTo = today.AddDays(-30);
                    break;
                case "critical":
                    filter.DateFrom = today.AddDays(-365);
                    filter.DateTo = today.AddDays(-60);
                    break;
                case "prenatalCheckup": // 產前孕前
                    filter.DateFrom = today.AddDays(-30);
                    filter.DateTo = today;
                    filter.ProductName = new List<string>{"慧智帶因篩檢 v1.0/ v2.0/ v3.0",
                                                            "海洋性貧血基因檢測-HBA、HBB基因",
                                                            "脊髓性肌肉萎縮症基因檢測-SMN基因 (SMA)",
                                                            "X染色體脆折症基因檢測-FMR1基因 (FXS)",
                                                            "慧智非侵產前染色體篩檢 v1.0/ v2.0/ v3.0 (NIPS)",
                                                            "慧智全方位複合式晶片檢測 v1.0/ v2.0/ v3.0 (Array)",
                                                            "細胞染色體檢查",
                                                            "母血唐氏症篩檢 (FDS)",
                                                            "子癲前症風險評估 (PE)",
                                                            "葉酸代謝基因檢測-MTHFR基因 (Folate)",
                                                            "先天性感染篩檢 (TORCH)"};
                    break;
                case "newborn": // 新生兒
                    filter.DateFrom = today.AddDays(-30);
                    filter.DateTo = today;
                    filter.ProductName = new List<string>{"慧智新生兒基因篩檢 v1.0/ v2.0/ v3.0",
                                                            "異位性皮膚炎過敏基因檢測-FLG基因 (AD)",
                                                            "感覺神經性聽損基因檢測",
                                                            "天中樞性換氣不足症候群基因檢測 (CCHS)",
                                                            "先天性巨細胞病毒感染檢測 (CMV)"};
                    break;
                case "rareDisease": // 罕見疾病
                    filter.DateFrom = today.AddDays(-30);
                    filter.DateTo = today;
                    filter.ProductName = new List<string>{"聽損基因檢測 v1.0/ v2.0/ v3.0",
                                                            "慧智單基因檢測",
                                                            "全外顯子定序基因檢測 (WES)",
                                                            "親緣鑑定檢測 (PT)"};
                    break;
                case "cancer": // 癌症
                    filter.DateFrom = today.AddDays(-30);
                    filter.DateTo = today;
                    filter.ProductName = new List<string>{"慧智癌症基因篩檢",
                                                            "人類乳突病毒篩檢 (HPV)",
                                                            "慧智癌風險基因檢測 v1.0/ v2.0",
                                                            "慧智癌風險-BRCA1/2基因檢測",
                                                            "慧智癌風險-大腸癌基因檢測",
                                                            "慧智癌風險-婦癌基因檢測"};
                    break;
                case "preciseMedication": // 精準用藥
                    filter.DateFrom = today.AddDays(-30);
                    filter.DateTo = today;
                    filter.ProductName = new List<string>{"慧智CGP癌症基因檢測",
                                                            "慧智癌監控基因檢測 v1.0/ v2.1",
                                                            "慧智HRD檢測",
                                                            "慧智癌監控基因檢測-BRCA1/2",
                                                            "微衛星不穩定檢測 (MSI)",
                                                            "慧智癌監控基因檢測 v2.2/ v3.0",
                                                            "慧智癌追蹤/慧智癌症特定基因檢測套組",
                                                            "慧智基因 癌監控基因檢測v2.2 x 國泰人壽醫心康愛防癌定期健康保險(外溢型)(實物給付型保險商品)",
                                                            "子宮內膜癌基因分型",
                                                            "攝護腺癌基因檢測",
                                                            "慧智癌監控基因檢測-肺癌",
                                                            "慧智癌監控基因檢測-乳癌",
                                                            "慧智癌監控基因檢測-大腸癌",
                                                            "慧智癌監控基因檢測-膽管癌",
                                                            "慧智癌監控基因檢測-泌尿道上皮癌",
                                                            "阿茲海默症​基因檢測-APOE"};
                    break;
                case "reproductiveMedicine": // 生殖醫學
                    filter.DateFrom = today.AddDays(-30);
                    filter.DateTo = today;
                    filter.ProductName = new List<string>{"胚胎著床前染色體篩檢 (PGT-A)","非侵入性胚胎著床前染色體篩檢 (niPGT-A)","胚胎著床前單基因檢測 (PGT-M)"};
                    break;
            }

            return filter;
        }
    }
}