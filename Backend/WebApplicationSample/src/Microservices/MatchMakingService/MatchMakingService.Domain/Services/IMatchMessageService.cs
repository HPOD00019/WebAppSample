using MatchMakingService.Domain.Entities;

namespace MatchMakingService.Domain.Services
{
    public interface IMatchMessageService
    {
        Task PublishNewMatchRequest(Match match);
    }
}
