
using MatchMakingService.Domain.TimeControls;

namespace MatchMakingService.Domain.Entities
{

    public class User
    {
        public int Id { get; set; }
        public int? BlitzRating { get; set; }
        public int? RapidRating { get; set; }
        public int? ClassicalRating { get; set; }

        public int GetRatingByTimeControl(TimeControl control)
        {
            var gameType = control.GetGameType();
            if (gameType == "Blitz") return BlitzRating.Value;
            if (gameType == "Rapid") return RapidRating.Value;
            if (gameType == "Classical") return ClassicalRating.Value;

            throw new Exception("Game type unknown at User!");
        }
        public void SetRatingByTimeControl(TimeControl control, int rating)
        {
            var gameType = control.GetGameType();

            if (gameType == "Blitz") {BlitzRating = rating; return; }
            if (gameType == "Rapid") {RapidRating = rating; return; }
            if (gameType == "Classical") {ClassicalRating = rating; return; }

            throw new Exception("Game type unknown at User!");
        }
    }
}
