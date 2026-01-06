using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultRecordController : ControllerBase
    {
        private readonly IConsultRecordService _service;

        public ConsultRecordController(IConsultRecordService service)
        {
            _service = service;
        }

        /// 取得某報告的所有諮詢紀錄
        [HttpGet("{reportId}")]
        public async Task<IActionResult> GetRecords(int reportId)
        {
            var records = await _service.GetRecordsAsync(reportId);
            return Ok(records);
        }

        /// 新增諮詢紀錄
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRecordDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = await _service.CreateRecordAsync(dto.ReportId, dto.Name, dto.Content);
            return Ok(new { id });
        }

        /// 更新諮詢紀錄
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRecordDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.UpdateRecordAsync(id, dto.Content);
            return Ok();
        }

        /// 刪除諮詢紀錄 (軟刪除)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteRecordAsync(id);
            return Ok();
        }
    }

    // DTOs
    public class CreateRecordDto
    {
        public int ReportId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    public class UpdateRecordDto
    {
        public string Content { get; set; } = string.Empty;
    }
}