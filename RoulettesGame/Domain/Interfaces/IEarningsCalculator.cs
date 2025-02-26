using RoulettesGame.Models;

namespace RoulettesGame.Domain.Interfaces
{
    public interface IEarningsCalculator
    {
        List<Bet> CalculateEarnings(IEnumerable<Bet> betList, int resultNumber);
    }
}
