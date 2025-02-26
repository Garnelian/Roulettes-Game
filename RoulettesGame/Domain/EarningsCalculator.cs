using RoulettesGame.Domain.Interfaces;
using RoulettesGame.Models;
using RoulettesGame.Models.Enums;
using RoulettesGame.Shared;
using System.Security.Principal;

namespace RoulettesGame.Domain
{
    public class EarningsCalculator : IEarningsCalculator
    {
        public List<Bet> CalculateEarnings(IEnumerable<Bet> betList, int resultNumber)
        {

            ColorBet winningColor = Rules.ColorBetByNumber(resultNumber);

            foreach (var bet in betList)
            {
                decimal earnigs = 0, totalEarnings = bet.Amount;

                if (bet.BetType == BetType.NumberBet && bet.Number == resultNumber)
                    earnigs += (bet.Amount * Constants.GAIN_PER_NUMBER);


                if (bet.BetType == BetType.ColorBet && bet.ColorBet == winningColor)
                    earnigs += bet.Amount * Constants.GAIN_PER_COLOR;

                bet.TotalEarnings = (earnigs > 0) ? earnigs : -totalEarnings;

            }

            return betList.ToList();
        }
    }
}
