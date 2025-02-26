using RoulettesGame.Models;

namespace RoulettesGame.Data.Repositories.Interfaces
{
    public interface IRoulleteRepository : IRepository<Roulette>
    {
        IEnumerable<Roulette> GetRuletteRounds(int[]? rouletteIds = null, bool? isActive = null);

        Task<IEnumerable<Roulette>> CloseRulettesAsync(int[] rouletteIds);
    }
}
