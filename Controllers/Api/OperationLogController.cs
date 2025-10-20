using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Controllers.api;

[Route("api/log")]
        public class OperationLogController : Controller
        {
            private readonly IOperationLogService _service;

            public OperationLogController(IOperationLogService service)
            {
                _service = service;
            }

            [HttpGet("search")]
            public IActionResult Search(string userId, string userName, string module, DateTime? from, DateTime? to)
            {
                try
                {
                    var logs = _service.Search(userId, userName, module, from, to);
                    return Ok(logs);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { error = ex.Message });
                }

            }
        }