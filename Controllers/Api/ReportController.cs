using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Controllers.api
{
    [ApiController]
    [Route("api/report")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;


        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public IActionResult GetReports([FromQuery] ReportFilter filter)
        {
            try
            {
                var result = _reportService.GetReports(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"錯誤:{ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetReport(int id)
        {
            var report = _reportService.GetReportById(id);
            if (report == null)
                return NotFound();

            return Ok(report);
        }

        [HttpPost]
        public IActionResult CreateReport([FromBody] Report report)
        {
            try
            {
                _reportService.CreateReport(report);
                return Ok("新增成功");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpPut("/api/report/{id}")]
        public IActionResult UpdateReport(int id, [FromBody] ReportUpdateDto updated)
        {
            if (updated == null || updated.Id != id)
                return BadRequest("報告資料無效");

            var success = _reportService.UpdateReport(updated);
            if (!success)
                return StatusCode(500, "更新失敗");

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteReport(int id)
        {
            try
            {
                _reportService.DeleteReport(id);
                return Ok("刪除成功");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpGet("/Report/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var report = _reportService.GetReportById(id);
            return View(report); // 傳給 Razor View
        }

        [HttpGet("/api/report/specimen-types/{columnName}")]
        public IActionResult GetSpecimenTypes(string columnName)
        {
            var types = _reportService.GetSpecimenTypeOptions(columnName);
            return Ok(types);
        }
        
        [HttpGet("/api/report/export")]
        public IActionResult Export([FromQuery] ReportFilter filter)
        {
            try
            {
               var filters = new ReportFilter
                {
                    TestItem = filter.TestItem,
                    DateFrom = filter.DateFrom,
                    DateTo = filter.DateTo,
                    Keyword = filter.Keyword,
                    NotifyStatus = filter.NotifyList ?? new List<string>(),
                    TrackStatus = filter.TrackList ?? new List<string>(),
                    Page = 1,
                    PageSize = int.MaxValue
                };

                var fileBytes = _reportService.ExportReports(filters);
                var filename = $"reports_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                return File(fileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    filename);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"匯出失敗: {ex.Message}");
            }

        }
    }
}
