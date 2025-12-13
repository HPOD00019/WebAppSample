using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransit.Conracts.Events
{
    public record MatchRequestHandledEvent
    {
        public Guid CorrelationId { get; init; } 
        public int MatchId { get; init; }
        public string? ErrorCode { get; init; }
        public bool IsSuccess { get; init; }
        public DateTime TimeStamp { get; init; }

    }
}
