using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Domain.Services;
using MediatR;

namespace AuthService.Application.Commands
{
    public record GetUserNameByIdCommand : IRequest<IResult<string>>
    {
        [Required] public int Id { get; set; }
    }
}
