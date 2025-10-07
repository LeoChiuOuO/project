using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.DTO;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Controllers.api
{
    [ApiController]
    [Route("api/report")]
    public class ReportController : ControllerBase
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

        [HttpPost("search")]
        public IActionResult Search([FromBody] ReportFilter filter)
        {
            var reports = _reportService.SearchReports(filter);
            if (reports == null) return NotFound();

            return Ok(reports);
        }

        [HttpPost("create")]
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
                    TestItemIds = filter.TestItemIds ?? new List<int>(),
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
        [HttpPost("/api/email/send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDTO dto)
        {
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress(dto.Sender);
                message.To.Add(dto.Recipient);
                message.Subject = "異常個案追蹤通知";
                message.Body = dto.Content;
                message.IsBodyHtml = false;

                using var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("leochiuouo@gmail.com", "mirs mhzf zsvv szee"),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(message);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
