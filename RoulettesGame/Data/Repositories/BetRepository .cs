using Microsoft.EntityFrameworkCore;
using RoulettesGame.Models;

namespace RoulettesGame.Data.Repositories
{
    public class BetRepository : Repository<Bet>, IBetRepository
    {
        public BetRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Bet>> GetBetsWithRoundsAndRoulleteAsync(int[]? RouletteIds = null, bool? isActive = null)
        {
            var query = _context.Bet
                        .Include(b => b.Round)
                        .ThenInclude(r => r.Roullette)
                        .AsQueryable();

            if (RouletteIds != null && RouletteIds.Length > 0)
            {
                query = query.Where(b => RouletteIds.Contains(b.Round.RoulletteId));
            }

            if (isActive.HasValue)
            {
                query = query.Where(b => b.Active == isActive.Value);
            }

            return await query.ToListAsync();
        }
    }
}
