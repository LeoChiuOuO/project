using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication_Dianthus.Models.Service.Interface;
using WebApplication_Dianthus.Models;
using ClosedXML.Excel;

namespace WebApplication_Dianthus.Controllers
{
    public class OBPatientController : Controller
    {
        private readonly IOBPatientService _obPatientService;
        private readonly ILogger<OBPatientController> _logger;

        public OBPatientController(IOBPatientService obPatientService, ILogger<OBPatientController> logger)
        {
            _obPatientService = obPatientService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(OBPatientSearchCriteria criteria)
        {
            var viewModel = new OBPatientViewModel();

            try
            {
                // Load available companies for dropdown
                ViewBag.Companies = await _obPatientService.GetAvailableCompaniesAsync();

                // Set default date range (current month)
                viewModel.SearchCriteria.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                viewModel.SearchCriteria.EndDate = DateTime.Now.Date;
                criteria.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                criteria.EndDate = DateTime.Now.Date;
                // Get patient records
                var records = await _obPatientService.GetPatientRecordsAsync(criteria);
                viewModel.Records = records;
                viewModel.TotalRecords = records.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading initial data");
                viewModel.ErrorMessage = "載入初始資料時發生錯誤";
                ViewBag.Companies = new List<string> { "民權" };
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Search(OBPatientSearchCriteria criteria)
        {
            var viewModel = new OBPatientViewModel { SearchCriteria = criteria };

            try
            {
                if (ModelState.IsValid)
                {
                    if (criteria.StartDate > criteria.EndDate)
                    {
                        ModelState.AddModelError("", "起始日期不能大於結束日期");
                        ViewBag.Companies = await _obPatientService.GetAvailableCompaniesAsync();
                        return View("Index", viewModel);
                    }

                    // Get patient records
                    var records = await _obPatientService.GetPatientRecordsAsync(criteria);
                    viewModel.Records = records;
                    viewModel.TotalRecords = records.Count;

                    // Get statistics
                    viewModel.Statistics = await _obPatientService.GetStatisticsAsync(criteria);

                    _logger.LogInformation($"Search completed: {records.Count} records found");
                }
                else
                {
                    viewModel.ErrorMessage = "請檢查搜尋條件是否正確填寫";
                }

                ViewBag.Companies = await _obPatientService.GetAvailableCompaniesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during search");
                viewModel.ErrorMessage = $"搜尋時發生錯誤: {ex.Message}";
                ViewBag.Companies = new List<string> { "民權" };
            }

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ExportToJson(string criteriaJson)
        {
            try
            {
                var criteria = JsonSerializer.Deserialize<OBPatientSearchCriteria>(criteriaJson);
                if (criteria == null) return BadRequest("Invalid search criteria");

                var records = await _obPatientService.GetPatientRecordsAsync(criteria);

                var fileName = $"OB病患記錄_{criteria.StartDate:yyyyMMdd}-{criteria.EndDate:yyyyMMdd}.json";
                var jsonData = JsonSerializer.Serialize(records, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                return File(System.Text.Encoding.UTF8.GetBytes(jsonData), "application/json", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting data");
                return BadRequest("匯出資料時發生錯誤");
            }
        }

        [HttpGet]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                var isConnected = await _obPatientService.TestConnectionAsync();
                return Json(new { success = isConnected, message = isConnected ? "連線成功" : "連線失敗" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Connection test error");
                return Json(new { success = false, message = $"連線測試錯誤: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel(string criteriaJson)
        {
            try
            {
                // 1. 解析搜尋條件
                var criteria = JsonSerializer.Deserialize<OBPatientSearchCriteria>(criteriaJson);
                if (criteria == null) return BadRequest("Invalid search criteria");
                // 2. 取得資料
                var records = await _obPatientService.GetPatientRecordsAsync(criteria);
                // 3. 產生 Excel
                using var workbook = new XLWorkbook();
                var ws = workbook.AddWorksheet("OB住院資料");
                // 標題列
                string[] columns = { "病歷號", "姓名", "床號", "入住日期", "離開日期", "分娩方式", "產後床號", "產後床入住日期", "產後床離開日期", "原始入住日期", "轉床否" };
                for (int i = 0; i < columns.Length; i++)
                    ws.Cell(1, i + 1).Value = columns[i];
                // 資料列
                for (int row = 0; row < records.Count; row++)
                {
                    var r = records[row];
                    ws.Cell(row + 2, 1).Value = r.病歷號;
                    ws.Cell(row + 2, 2).Value = r.姓名;
                    ws.Cell(row + 2, 3).Value = r.床號;
                    ws.Cell(row + 2, 4).Value = r.入住日期?.ToString("yyyy/MM/dd HH:mm") ?? "";
                    ws.Cell(row + 2, 5).Value = r.離開日期?.ToString("yyyy/MM/dd HH:mm") ?? "";
                    ws.Cell(row + 2, 6).Value = r.分娩方式;
                    ws.Cell(row + 2, 7).Value = r.產後床號;
                    ws.Cell(row + 2, 8).Value = r.產後床入住日期?.ToString("yyyy/MM/dd HH:mm") ?? "";
                    ws.Cell(row + 2, 9).Value = r.產後床離開日期?.ToString("yyyy/MM/dd HH:mm") ?? "";
                    ws.Cell(row + 2, 10).Value = r.原始入住日期?.ToString("yyyy/MM/dd HH:mm") ?? "";
                    ws.Cell(row + 2, 11).Value = r.轉床否;
                }
                ws.Columns().AdjustToContents();
                
                // 將 MemoryStream 轉換為 byte array
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    fileBytes = ms.ToArray(); // 在 using 內轉換為 byte array
                }
                var fileName = $"OB住院資料_{criteria.StartDate:yyyyMMdd}_{criteria.EndDate:yyyyMMdd}.xlsx";
                
                // 使用 byte array 回傳文件
                return File(fileBytes, 
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                        fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting to Excel");
                return BadRequest("匯出 Excel 發生錯誤");
            }
        }
    }
}


