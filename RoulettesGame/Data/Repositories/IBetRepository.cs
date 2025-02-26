using RoulettesGame.Models;
using System.Security;

namespace RoulettesGame.Data.Repositories
{
    public interface IBetRepository : IRepository<Bet>
    {
        Task<IEnumerable<Bet>> GetBetsWithRoundsAndRoulleteAsync(int[]? RouletteIds = null, bool? isActive= null);
    }
}
