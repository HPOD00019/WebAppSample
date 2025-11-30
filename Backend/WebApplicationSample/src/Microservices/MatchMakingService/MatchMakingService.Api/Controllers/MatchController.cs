using Microsoft.AspNetCore.Mvc;

namespace MatchMakingService.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MatchController : ControllerBase
    {
        [Route("NewGame")]
        [HttpPost]
        public IActionResult RequestNewMatch()
        {
            
            return Ok();
        }
    }
}
