using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransit.Conracts.Commands
{
    public record CreateNewMatchCommand
    {
        public Guid CorrelationId { get; init; }
        public int MatchId { get; init; }
        public int WhiteOpponentId { get; init; }
        public int BlackOpponentId { get; init; }
        public DateTime TimeStamp { get; init; }
        public int TimeControl {  get; init; }

    }
}
