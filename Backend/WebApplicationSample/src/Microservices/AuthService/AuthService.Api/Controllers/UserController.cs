using AuthService.Api.DTOs;
using AuthService.Application.Commands;
using AuthService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetUserName")]
        public async Task<IActionResult> GetUserName([FromQuery] int Id)
        {
            var request = new GetUserNameByIdCommand
            {
                Id = Id
            };
            var name = await _mediator.Send(request);
            return Ok(name);
        }
        [HttpGet("GetPlayer")]
        public async Task<IActionResult> GetPlayer([FromQuery] int id)
        {
            var query = new GetUserAsPlayerQuery
            {
                userId = id
            };

            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                var response = new ApiResponse
                {
                    Success = true,
                    Date = DateTime.Now,
                };
                var data = new
                {
                    Id = result.Value.Id,
                    Rating = result.Value.Rating,
                };
                response.Data = data;
                return Ok(response);
            }
            else
            {
                var response = new ApiResponse
                {
                    Success = false,
                    ErrorCode = result.EmergedError.code.ToString(),
                    Message = result.EmergedError.Message?.ToString(),
                    Date = DateTime.Now,
                };
                return BadRequest(response);
            }
        }
    }
}
