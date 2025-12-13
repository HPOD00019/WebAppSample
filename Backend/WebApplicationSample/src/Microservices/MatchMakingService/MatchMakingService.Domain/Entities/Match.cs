using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchMakingService.Domain.TimeControls;

namespace MatchMakingService.Domain.Entities
{
    public record Match
    {
        public int Id { get; set; }
        public int White { get; set; }
        public int Black { get; set; }
        public TimeControl control { get; set; }
    }
}
