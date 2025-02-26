using RoulettesGame.Models;
using System.Security;

namespace RoulettesGame.Data.Repositories.Interfaces
{
    public interface IBetRepository : IRepository<Bet>
    {
        IEnumerable<Bet> GetBetsByRoulleteId(int[]? RouletteIds = null, bool? isActive = true);
    }
}
