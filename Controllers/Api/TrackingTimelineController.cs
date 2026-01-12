using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingTimelineController : ControllerBase
    {
        private readonly ITrackingTimelineService _service;

        public TrackingTimelineController(ITrackingTimelineService service)
        {
            _service = service;
        }

        // 建立
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrackingTimeline dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CreateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"伺服器錯誤: {ex.Message}");
            }
        }

        // 更新
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrackingTimeline dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.UpdateAsync(id, dto);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"伺服器錯誤: {ex.Message}");
            }
        }

        // 依 ReportId 讀取
        [HttpGet("report/{reportId}")]
        public async Task<IActionResult> GetByReportId(int reportId)
        {
            var items = await _service.GetByReportIdAsync(reportId);
            return Ok(items);
        }

        // 依 Id 讀取
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }
    }
}