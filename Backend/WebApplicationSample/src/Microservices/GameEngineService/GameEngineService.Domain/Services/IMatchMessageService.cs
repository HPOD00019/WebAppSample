using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngineService.Domain.Chess;

namespace GameEngineService.Domain.Services
{
    public interface IMatchMessageService
    {
        Task PublishMatchCreatedMessage(int id);
    }
}
