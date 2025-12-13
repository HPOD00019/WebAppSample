using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Services;
using MediatR;

namespace GameEngineService.Application.Commands
{
    public record MatchCreateRequestCommand : IRequest<IResult<IGameSession>>
    {
        [Required] public int Id { get; set; }
        [Required] public int Black { get; set; }
        [Required] public int White { get; set; }
        [Required] public int Control { get; set; }
    }
}
