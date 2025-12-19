using MatchMakingService.Api.DTOs;
using MatchMakingService.Api.HttpClientsServices;
using MatchMakingService.Application.Commands;
using MatchMakingService.Domain.Services;
using MatchMakingService.Domain.TimeControls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MatchMakingService.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MatchController : ControllerBase
    {
        private IMediator _mediator;
        private UserServiceClient _userServiceClient;
        public MatchController(IMediator mediator, UserServiceClient client)
        {
            
            _mediator = mediator;
            _userServiceClient = client;
        }

        [Route("PingMatchRequest")]
        [HttpGet]
        public async Task<IActionResult> PingRequestMatch([FromQuery] TimeControl _control)
        {
            var isIdFound = HttpContext.Items.TryGetValue("UserId", out var userId);
            if (!isIdFound) throw new Exception("user id was not found at match controller");

            var request = new UpdateMatchRequestTTLcommand
            {
                IssuerId = (int)userId,
                control = _control,
            };

            var result = await _mediator.Send(request);
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
                };
                return Ok(response);
            }
            return Ok();
        }

        [Route("NewGame")]
        [HttpGet]
        public async Task<IActionResult> RequestNewMatch([FromQuery] TimeControl _control)
        {
            var isIdFound = HttpContext.Items.TryGetValue("UserId", out var userId);
            if (!isIdFound) throw new Exception("user id was not found at match controller");

            var id = (int)userId;
            var issuer = await _userServiceClient.GetUser(id);
            var request = new CreateMatchRequestCommand
            {
                Issuer = issuer,
                control = _control,
            };

            var result = await _mediator.Send(request);
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
                throw new Exception(result.ErrorMessage);
            }

        }
    }
}
