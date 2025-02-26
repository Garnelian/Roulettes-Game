using RoulettesGame.Models;
using System.Security;

namespace RoulettesGame.Data.Repositories.Interfaces
{
    public interface IRoundRepository : IRepository<Round>
    {
        IEnumerable<Round> GetRoundsByRouletteId(int rouletteId);
        Task<Round?> GetActiveRoundByRouletteIdAsync(int rouletteId);
        List<RoundResult> GetRoundResultsByRoundId(int roundId);

    }
}
