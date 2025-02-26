using Microsoft.EntityFrameworkCore;
using RoulettesGame.Data.Repositories.Interfaces;
using RoulettesGame.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RoulettesGame.Data.Repositories
{
    public class RouletteRepository : Repository<Roulette>, IRoulleteRepository
    {
        public RouletteRepository(ApplicationDbContext context) : base(context) { }

        public IEnumerable<Roulette> GetRuletteRounds(int[]? rouletteIds = null, bool? isActive = null)
        {

            var query = _context.Roullete
                       .Include(b => b.Rounds)
                       .AsQueryable();

            if (rouletteIds != null && rouletteIds.Length > 0)
            {
                query = query.Where(x => rouletteIds.Contains(x.Id));
            }

            if (isActive.HasValue)
            {
                query = query.Where(b => b.Active == isActive.Value);
            }

            return query;
        }

        public async Task<IEnumerable<Roulette>> CloseRulettesAsync(int[] rouletteIds)
        {
            var roulletes = _context.Roullete?
                           .Include(h => h.Rounds)
                           .ThenInclude(n => n.Bets)
                           .Where(x=> rouletteIds.Contains(x.Id));

            if (roulletes != null)
            {
                foreach (var roullet in roulletes)
                {
                    roullet.Rounds
                .ForEach(r =>
                {
                    r.Bets.ForEach(b => b.Active = false);
                    r.Active = false;
                }
                );
                }
            }

            return await roulletes.ToListAsync();
        }
    }
}
