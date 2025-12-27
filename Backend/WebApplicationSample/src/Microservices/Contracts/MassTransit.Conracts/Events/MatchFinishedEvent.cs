using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransit.Conracts.Events
{
    public record MatchFinishedEvent
    {
        public Guid CorrelationId { get; init; }
        public int MatchId { get; init; }
        public DateTime TimeStamp { get; init; }
        public int matchResult { get; init; }
        public int timeControl { get; init; }
    }
}
