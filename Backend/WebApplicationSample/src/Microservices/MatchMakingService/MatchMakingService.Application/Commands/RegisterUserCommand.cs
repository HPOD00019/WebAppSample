using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchMakingService.Domain.Entities;
using MediatR;

namespace MatchMakingService.Application.Commands
{
    public record RegisterUserCommand : IRequest<IResult<int>>
    {
        public User user { get; set; }
    }
}
