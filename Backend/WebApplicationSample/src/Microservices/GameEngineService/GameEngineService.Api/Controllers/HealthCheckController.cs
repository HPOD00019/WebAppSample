using GameEngineService.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameEngineService.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private IMatchMessageService _service;
        public HealthCheckController(IMatchMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        
        public IActionResult TestMassTransit([FromQuery] int id)
        {
            _service.PublishMatchCreatedMessage(id);
            return Ok();
        }
    }
}
