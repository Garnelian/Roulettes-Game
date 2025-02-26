using Microsoft.EntityFrameworkCore;
using RoulettesGame.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RoulettesGame.Data.Repositories
{
    public class RouletteRepository : Repository<Roulette>, IRoulleteRepository
    {
        public RouletteRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Roulette>> GetRuletteRoundsAsync(int[]? rouletteIds = null, bool? isActive = null)
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

            return await query.ToListAsync();
        }
    }
}
