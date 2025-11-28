using Microsoft.AspNetCore.Mvc;

namespace MatchMakingService.API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MatchMakingContoller : ControllerBase
    {
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
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
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
