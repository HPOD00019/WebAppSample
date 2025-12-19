using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthService.Application.Commands.Register;
using AuthService.Api.DTOs;
using AuthService.Domain.Services;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using AuthService.Application.Commands.GenerateToken;
using AuthMiddleware.Entities;
using System.Text.Json;
using AuthService.Application.Commands.TokenValidation;
using Microsoft.AspNetCore.Cors;

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
        [HttpGet("RefreshAccessToken")]
        [EnableCors("WithCredentials")]
        public async Task<IActionResult> RefreshAccessToken([FromQuery] string refreshToken)
        {

            var command = new GenerateOrGetAccessTokenCommand
            {
                RefreshToken = refreshToken,
            };

            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                var response = new ApiResponse
                {
                    Date = DateTime.Now,
                    Data = result.Value,
                    Success = true,
                };
                return Ok(response);
            }
            else
            {
                var response = new ApiResponse
                {
                    Date = DateTime.Now,
                    Success = false,
                    ErrorCode = result.EmergedError.code.ToString(),
                };
                return Ok(response);
            }
        }
        [HttpGet("verifyAccessToken")]
        public async Task<IActionResult> VerifyToken([FromQuery] string token)
        {
            var command = new AccessTokenValidateCommand
            {
                AccessToken = token,
            };
            var validationResult = await _mediator.Send(command);

            if (validationResult.IsSuccess)
            {
                var response = new AuthResult();
                response.IsValid = true;
                response.UserId = validationResult.Value.Id;

                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Register")]
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
                var refreshTokenRequest = new GenerateOrGetRefreshTokenCommand
                {
                    UserId = result.Value,
                };
                var refreshTokenResult = await _mediator.Send(refreshTokenRequest);
                if (refreshTokenResult.IsSuccess)
                {
                    var accessTokenRequest = new GenerateOrGetAccessTokenCommand
                    {
                        RefreshToken = refreshTokenResult.Value,
                    };
                    var accessTokenResult = await _mediator.Send(accessTokenRequest);
                    if (accessTokenResult.IsSuccess)
                    {
                        var data = new UserRegistrationResponse 
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Id = result.Value,
                            AccessToken = accessTokenResult.Value,
                            RefreshToken = refreshTokenResult.Value,
                        };
                        var response = new ApiResponse
                        {
                            Data = data,
                            Date = DateTime.Now,
                            Success = true,
                        };
                        Response.Cookies.Append("RefreshToken", accessTokenResult.Value, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = false,
                            Path = "/Auth/RefreshAccessToken",
                            IsEssential = true,
                        });
                        return Ok(response);
                    }
                }
            }
            else
            {
                var response = new ApiResponse
                {
                    Date = DateTime.Now,
                    Success = false,
                    ErrorCode = result.EmergedError.code.ToString()
                };
                return Ok(response);
            }
            return Ok();
        }

        [HttpPost("Login")]
        [EnableCors("WithCredentials")]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            var command = new LoginUserCommand
            {
                UserName = user.UserName,
                Password = user.Password,
            };
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                var response = new ApiResponse
                {
                    Date = DateTime.Now,
                    Success = false,
                    ErrorCode = result.EmergedError.code.ToString(),
                };
                return Ok(response);
            }
            else
            {
                var response = new ApiResponse
                {
                    Date = DateTime.Now,
                    Success = true,
                    Data = result.Value,
                };
                Response.Cookies.Append("RefreshToken", result.Value, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    Path = "/Auth/RefreshAccessToken",
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax,
                });
                return Ok(response);
            }
            
        }
    }
}
