using Microsoft.EntityFrameworkCore;
using RoulettesGame.Data.Repositories.Interfaces;
using RoulettesGame.Models;

namespace RoulettesGame.Data.Repositories
{
    public class RoundRepository : Repository<Round>, IRoundRepository
    {
        public RoundRepository(ApplicationDbContext context) : base(context) { }

        public IEnumerable<Round> GetRoundsByRouletteId(int rouletteId)
        {
            return _context.Round
                           .Where(x => x.RoulletteId == rouletteId);
        }

        public Task<Round?> GetActiveRoundByRouletteIdAsync(int rouletteId)
        {
            return _context.Round
                   .Where(x => x.RoulletteId == rouletteId && x.Active)
                   .OrderBy(x => x.Id)
                   .LastOrDefaultAsync();
        }

        public List<RoundResult> GetRoundResultsByRoundId(int roundId)
        {
            return _context.Bet
                           .Where(b => b.TotalEarnings.HasValue && b.RoundId==roundId)
                           .GroupBy(b => b.User)
                           .Select(g => new RoundResult
                           {
                               User = g.Key ?? "UNKNOWN",
                               NetEarnings = g.Sum(b => b.TotalEarnings ?? 0)
                           })
                           .ToList();
        }

    }
}
