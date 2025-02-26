using RoulettesGame.Models;

namespace RoulettesGame.Data.Repositories
{
    public interface IRoulleteRepository : IRepository<Roulette>
    {
        Task<IEnumerable<Roulette>> GetRuletteRoundsAsync(int[]? rouletteIds = null, bool? isActive = null);
    }
}
