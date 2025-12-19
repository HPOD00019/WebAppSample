using MatchMakingService.Api.DTOs;
using MatchMakingService.Application.Commands;
using MatchMakingService.Domain.Entities;
using MatchMakingService.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MatchMakingService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var command = new RegisterUserCommand
            {
                user = user
            };
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                var response = new ApiResponse
                {
                    Success = true,
                    Data = result.Value
                };
                return Ok(response);
            }
            else
            {
                throw new Exception(result.EmergedError.code.ToString());
            }
        }
    }
}
