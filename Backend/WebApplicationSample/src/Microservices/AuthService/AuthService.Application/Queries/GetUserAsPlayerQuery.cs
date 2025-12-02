using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Domain.Models;
using AuthService.Domain.Services;
using MediatR;

namespace AuthService.Application.Queries
{
    public record GetUserAsPlayerQuery : IRequest<IResult<User>>
    {
        [Required] public int userId { get; set; }
    }
}
