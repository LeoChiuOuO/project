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

        private readonly IOperationLogService _log;
        public ReportController(IReportService reportService, IOperationLogService log)
        {
            _reportService = reportService;
            _log = log;
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
            _log.Log(actionType: "Search",module: "ReportController", success: true, description: "搜尋成功");
            return Ok(reports);
        }

        [HttpPost("create")]
        public IActionResult CreateReport([FromBody] Report report)
        {
            try
            {
                _reportService.CreateReport(report);
                _log.Log(actionType: "CreateReport",module: "ReportController", success: true, description: "建立成功");
                return Ok("新增成功");
            }
            catch (UnauthorizedAccessException ex)
            {
                _log.Log(actionType: "CreateReport",module: "ReportController", success: false, description: "建立失敗:" + ex.Message);
                return Forbid(ex.Message);
            }
        }

        [HttpPut("/api/report/{id}")]
        public IActionResult UpdateReport(int id, [FromBody] ReportUpdateDto updated)
        {
            if (updated == null || updated.Id != id){
                _log.Log(actionType: "UpdateReport",module: "ReportController", success: false, description: "報告資料無效");
                return BadRequest("報告資料無效");
            }

            var success = _reportService.UpdateReport(updated);
            if (!success){
                _log.Log(actionType: "UpdateReport",module: "ReportController", success: success, description: "更新失敗");
                return StatusCode(500, "更新失敗");
            }
                
            _log.Log(actionType: "UpdateReport",module: "ReportController", success: success, description: "更新成功");
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
        
        [HttpPost("export/simplified")]
        public IActionResult ExportSimplified([FromBody] ReportFilter filter)
        {
            var file = _reportService.ExportSimplifiedReports(filter);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "簡化報告.xlsx");
        }

        [HttpPost("export/full")]
        public IActionResult ExportFull([FromBody] ReportFilter filter)
        {
            var file = _reportService.ExportFullReports(filter);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "完整報告.xlsx");
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
