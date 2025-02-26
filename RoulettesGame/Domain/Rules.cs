using RoulettesGame.Models.Enums;

namespace RoulettesGame.Domain
{
    public static class Rules
    {
        public static ColorBet ColorBetByNumber(int number)
        {
            return number % 2 == 0 ? ColorBet.Red : ColorBet.Black;
        }
    }
}
