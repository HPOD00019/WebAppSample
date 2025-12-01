using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakingService.Domain.Entities
{
    public class MatchRequest
    {
        private readonly User _issuer;
        private readonly int _minOpponentRating;
        private readonly int _maxOpponentRating;

        public MatchRequest(User issuer, int minOpponentRating, int maxOpponentRating)
        {
            _issuer = issuer;
            _minOpponentRating = minOpponentRating;
            _maxOpponentRating = maxOpponentRating;
        }

    }
}
