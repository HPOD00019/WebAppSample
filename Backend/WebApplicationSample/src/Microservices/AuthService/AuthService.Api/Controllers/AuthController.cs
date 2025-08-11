using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthService.Application.Commands.Register;
using AuthService.Api.DTOs;

namespace AuthService.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AuthController : ControllerBase
    {
        private IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO user)
        {

            var request = new RegisterUserCommand()
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
            };


            var id = await _mediator.Send(request);

            return Ok();
        }
    }
}
