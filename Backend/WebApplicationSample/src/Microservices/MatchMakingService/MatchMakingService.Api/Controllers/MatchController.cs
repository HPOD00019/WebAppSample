using MatchMakingService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MatchMakingService.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MatchController : ControllerBase
    {
        private IMediator _mediator;
        public MatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("PingMatchRequest")]
        [HttpGet]
        public async Task<IActionResult> PingRequestMatch([FromQuery] int id)
        {
            var request = new UpdateMatchRequestTTLcommand
            {
                IssuerId = id
            };

            var result = await _mediator.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                throw new Exception(result.ErrorMessage);
            }
            return Ok();
        }

        [Route("NewGame")]
        [HttpGet]
        public async Task<IActionResult> RequestNewMatch()
        {
            var isIdFound = HttpContext.Items.TryGetValue("UserId", out var userId);
            if (!isIdFound) throw new Exception("user id was not found at match controller");

            var id = (int)userId;
            var request = new CreateMatchRequestCommand
            {
                Issuer = id
            };

            var result = await _mediator.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                throw new Exception(result.ErrorMessage);
            }
            return Ok();
        }
    }
}
