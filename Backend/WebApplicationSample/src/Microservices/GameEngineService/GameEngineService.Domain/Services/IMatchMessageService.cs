using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineService.Domain.Chess;
using GameEngineService.Domain.Entities;
using GameEngineService.Domain.TimeControls;

namespace GameEngineService.Domain.Services
{
    public interface IMatchMessageService
    {
        Task PublishMatchCreatedMessage(int id);
        Task PublishMatchFinishedMessage(MatchResultDTO results);
    }
}
