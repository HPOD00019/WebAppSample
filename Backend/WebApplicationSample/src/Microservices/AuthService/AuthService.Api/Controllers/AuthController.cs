using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {

        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register()
        {

            return BadRequest();
        }
    }
}
