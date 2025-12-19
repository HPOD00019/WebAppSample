using GameEngineService.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameEngineService.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        public HealthCheckController()
        {
        }

    }
}
