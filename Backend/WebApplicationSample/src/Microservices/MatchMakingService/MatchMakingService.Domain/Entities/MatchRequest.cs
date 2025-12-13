using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakingService.Domain.Entities
{
    public record MatchRequest
    {
        private User _issuer;
        private int _minOpponentRating;
        private int _maxOpponentRating;

        public MatchRequest(User issuer, int minOpponentRating, int maxOpponentRating)
        {
            _issuer = issuer;
            _minOpponentRating = minOpponentRating;
            _maxOpponentRating = maxOpponentRating;
        }

    }
}
