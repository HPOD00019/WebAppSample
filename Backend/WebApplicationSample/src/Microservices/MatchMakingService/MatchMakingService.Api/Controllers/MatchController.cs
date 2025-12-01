using Microsoft.AspNetCore.Mvc;

namespace MatchMakingService.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MatchController : ControllerBase
    {
        [Route("NewGame")]
        [HttpGet]
        public IActionResult RequestNewMatch()
        {
            return Ok();
        }
    }
}
