using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineService.Application.Commands;
using GameEngineService.Application.Services;
using GameEngineService.Domain;
using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Connections;
using GameEngineService.Domain.Entities;
using GameEngineService.Domain.Services;
using GameEngineService.Domain.TimeControls;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GameEngineService.Application.Handlers
{
    public class MatchCreateRequestCommandHandler : IRequestHandler<MatchCreateRequestCommand, IResult<IGameSession>>
    {
        private IServiceProvider _provider;
        private SessionManager _manager;

        public MatchCreateRequestCommandHandler(SessionManager manager, IServiceProvider provider)
        {
            _manager = manager;
            _provider = provider;
        }

        public async Task<IResult<IGameSession>> Handle(MatchCreateRequestCommand request, CancellationToken cancellationToken)
        {
            var connection = _provider.GetRequiredService<IGameConnection>();
            var core = _provider.GetRequiredService<IChessCore>();

            var game = new WebSocketGameSession(connection, core, request.Id);

            
            _manager.AddSession(game);
            _manager.AddPlayer(request.White, request.Id);
            _manager.AddPlayer(request.Black, request.Id);
            var black = new User(request.Black);
            var white = new User(request.White);
            game.SetPlayers(black, white);
            game.SetTimeControl((TimeControl)request.Control);

            var result = Result<IGameSession>.OnSuccess(game);
            
            return result;
            
        }
    }
}
