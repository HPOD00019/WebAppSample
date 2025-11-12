using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthService.Application.Commands.Register;
using AuthService.Api.DTOs;
using AuthService.Domain.Services;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using AuthService.Application.Commands.GenerateToken;

namespace AuthService.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AuthController : ControllerBase
    {
        private IMediator _mediator;
        private ITokenService _tokenService;

        public AuthController(IMediator mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
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

            var result = await _mediator.Send(request);
            if (result.IsSuccess)
            {
                var refreshTokenRequest = new GenerateRefreshTokenCommand
                {
                    UserId = result.Value,
                };
                var refreshTokenResult = await _mediator.Send(refreshTokenRequest);
                if (refreshTokenResult.IsSuccess)
                {
                    var accessTokenRequest = new GenerateAccessTokenCommand
                    {
                        RefreshToken = refreshTokenResult.Value,
                    };
                    var accessTokenResult = await _mediator.Send(accessTokenRequest);
                    if (accessTokenResult.IsSuccess)
                    {
                        var response = new UserRegistrationResponse 
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Id = result.Value,
                            AccessToken = accessTokenResult.Value,
                            RefreshToken = refreshTokenResult.Value,
                        };
                        return Ok(response);
                    }
                }
            }
            else
            {

            }
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            var request = new 
        }
    }
}
