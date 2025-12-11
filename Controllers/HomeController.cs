using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Utilities;
using WebApplication_Dianthus.Models.Service.Interface;
using System.Net.Http;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly IDbConnection _db;
    private readonly IReportService _reportService;
    private readonly IHttpClientFactory _httpClientFactory;


    public HomeController(IDbConnection db, IReportService reportService, IHttpClientFactory httpClientFactory)
    {
        _db = db;
        _reportService = reportService;
        _httpClientFactory = httpClientFactory;
    }
    public IActionResult Index()
    {
        var dashboard = _reportService.GetDashboard();
        return View(dashboard);

    }

    public IActionResult Index2()
    {
        var dashboard = _reportService.GetDashboard();
        return View(dashboard);
    }

    public IActionResult leftMenu()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetDashboard()
    {
        var dashboard = _reportService.GetDashboard();
        return Json(dashboard);
    }

    [HttpGet]
    public async Task<IActionResult> testAPIAsync()
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Get, "https://specimen.sofiva.com.tw/Auth");
        // token 從安全設定讀取（示意）
        var token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJkYXRhIjp7ImNpZCI6NDEyLCJuYW1lIjoi56a-6aao6Yar55mC6ZuG5ZyYIiwic2VjcmV0IjoidG9rZW4iLCJhZnRlciI6IjIwMjQtMDQtMTAgMTc6MDY6NTUiLCJiZWZvcmUiOiIyMDM0LTA0LTEwIDE3OjA2OjU1In0sImlhdCI6MTcxMjc0MDAxNSwiZXhwIjoxNzEyNzQzNjE1fQ.ZwiMlFbBnN-xmVkswEVuIGa18rnBMeR5AYvwKAspEeadcrs-mcc5wLcL4h0KN_ni1CvRNece3NK6DPYSBgkhkGNBBYKIyp_X6he5HhGR_wu82EDpQpjP0AKwpR5D9RDaXrVyeWdHSMt9cnROP0mqoJj4tqFiNs8KdOP1M-gSuaDAXRww3XHLHSr030D01MvmXiMBelEtwpCAxxGOanOisZc9-C_LCcHQ5bK1O67emym8dCqM85QHqYM-Uj_z2ZT_s8kQV4xluo4aNTq51TDf_UPJpKO27n9QNGUubABNSDolsJ-DXWjQXJJBhZOB7xpZ02dm4J6SVhj1kVb0ZSVOUk4KkzRj4pfuIrXKCVYRUh0mrnY4sdRTpK6GoqKlv6zlT3zAG9uoX8jwBgatMg7XNlgb-6Fb6KOD4uUOYt4fb4-tDv23QVmxSSY1oGxVgRlOokrsReOemUXd0EiepiFN5CAQtT6Uyi6MhcpOvGxHksXQ0Vncg6UejN2WkcX7iWn7tZxOkt8t4sK3D_MZksm5NYkBKyA6-2blhb5nrzU7pEAQnY39IkIV2WaxfDPx_tiZj90yOWqpVAlwsVIWc80lj_Z-Th-WDlJ5TjnZMAzXCS-2_IVAtdWuY6vgkkvwzrb7jidLhAZtCO1VQRUS6TWJDinLGh8zViH3XzGCmxMEDOY";
        request.Headers.Add("X-Sofiva-OpenAPI-Token", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            // 回傳原始錯誤與狀態供前端顯示
            return StatusCode((int)response.StatusCode, content);
        }

        HttpResponseMessage result = new HttpResponseMessage();
        // 如果後端 API 回傳 JSON 字串且想直接傳給前端
        // 確保 content 是合法 JSON，並回傳 Content with application/json
        return StatusCode((int)response.StatusCode);
    }
}