using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoulettesGame.Data.UnitOfWork;
using RoulettesGame.Models;
using RoulettesGame.Shared;
using System.Reflection.Metadata;

namespace RoulettesGame.Controllers
{
    [Route("api/roulettes")]
    public class RouletteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RouletteController> _logger;

        public RouletteController(IUnitOfWork unitOfWork, ILogger<RouletteController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoulette([FromBody] Roulette roulette)
        {
            roulette.CreatedAt = DateTime.Now;

            await _unitOfWork.RouletteRepository.AddAsync(roulette);

            await _unitOfWork.CompleteAsync();

            return Ok(new { RouletteId = roulette.Id, Message = Constants.ROULETTE_CREATED_SUCCESSFULLY });
        }
        [HttpPost("{rouletteId}/rounds")]
        public async Task<IActionResult> CreateRound(int rouletteId)
        {
            var roulette = await _unitOfWork.RouletteRepository.GetByIdAsync(rouletteId);

            if (roulette == null)
                return BadRequest(new { Message = Constants.ROULETTE_NOT_FOUND });

            roulette.Active = true;

            Round round = new Round() { RoulletteId = rouletteId, CreatedAt = DateTime.Now };

            await _unitOfWork.RoundRepository.AddAsync(round);

            await _unitOfWork.CompleteAsync();

            return Ok(new { RouletteId = roulette.Id, Message = Constants.ROULETTE_OPENED_SUCCESSFULLY });
        }

        [HttpPost("{rouletteId}/bets")]
        public async Task<IActionResult> PlaceBet(int rouletteId, [FromBody] Bet bet, [FromHeader] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roulette = await _unitOfWork.RouletteRepository.GetRuletteRoundsAsync(new int[] { rouletteId }, true);

            if (!roulette.Any())
                return BadRequest(new { Message = Constants.ROULETTE_NOT_FOUND_OR_ALREADY_CLOSED });

            if (bet.Amount > Constants.MAX_BET_AMMOUNT || bet.Amount < Constants.MIN_BET_AMMOUNT)
                return BadRequest(new { Message = Constants.INVALID_BET_AMOUNT });


            bet.RoundId = roulette.FirstOrDefault().Rounds.LastOrDefault(x => x.Active == true).Id;
            bet.User = userId;

            await _unitOfWork.BetRepository.AddAsync(bet);

            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = Constants.BET_REGISTERED });
        }

        [HttpGet("{rouletteId}/close")]
        public async Task<IActionResult> CloseRoulette(int rouletteId)
        {
            var ActiveBetList = await _unitOfWork.BetRepository.GetBetsWithRoundsAndRoulleteAsync(new int[] { rouletteId }, true);
            if (!ActiveBetList.Any())
                return BadRequest(new { Message = "No Hay Apuestas Asociadas En Esta Ruleta" });

            int winningNumber = new Random().Next(Constants.MIN_NUMBER_RANGE, Constants.MAX_NUMBER_RANGE + 1);
            string winningColor = winningNumber % 2 == 0 ? "RED" : "BLACK";


            foreach (var bet in ActiveBetList)
            {
                bool wins = bet.Number.HasValue ? bet.Number.Value == winningNumber : bet.Color.ToUpper() == winningColor;
                bet.AmountWon = wins ? (bet.Number.HasValue ? bet.Amount * 5 : bet.Amount * 1.8m) : bet.Amount * -1;
                bet.Round.Active = false;
                bet.Round.Roullette.Active = false;
                bet.Active = false;
            }

            await _unitOfWork.CompleteAsync();

            var totalResults = ActiveBetList
                        .Where(b => b.AmountWon.HasValue)
                        .GroupBy(b => b.User)
                        .Select(g => new
                        {
                            User = g.Key,
                            TotalAmountWon = g.Sum(b => b.AmountWon ?? 0)
                        }).ToList();

            //roulette.Status = RouletteStatus.Closed;
            return Ok(new { WinningNumber = winningNumber, WinningColor = winningColor, Results = totalResults });
        }
    }


}
