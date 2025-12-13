using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineService.Application.Commands;
using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Services;
using MediatR;

namespace GameEngineService.Application.Handlers
{
    public class MatchCreateRequestCommandHandler : IRequestHandler<MatchCreateRequestCommand, IResult<IGameSession>>
    {
        public Task<IResult<IGameSession>> Handle(MatchCreateRequestCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
