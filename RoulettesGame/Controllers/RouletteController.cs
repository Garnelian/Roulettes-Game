using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoulettesGame.Data.UnitOfWork;
using RoulettesGame.Domain;
using RoulettesGame.Domain.Interfaces;
using RoulettesGame.Models;
using RoulettesGame.Models.Enums;
using RoulettesGame.Shared;
using System.Security.Cryptography;

namespace RoulettesGame.Controllers
{
    [Route("api/roulettes")]
    public class RouletteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEarningsCalculator _earningsCalculator;
        private readonly ILogger<RouletteController> _logger;

        public RouletteController(IUnitOfWork unitOfWork
                                  , IEarningsCalculator earningsCalculator
                                  , ILogger<RouletteController> logger)
        {
            _unitOfWork = unitOfWork;
            _earningsCalculator = earningsCalculator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoulette([FromBody] Roulette roulette)
        {
            try
            {
                roulette.CreatedAt = DateTime.Now;

                await _unitOfWork.RouletteRepository.AddAsync(roulette);

                await _unitOfWork.CompleteAsync();

                return Ok(new { RouletteId = roulette.Id, Message = Constants.ROULETTE_CREATED_SUCCESSFULLY });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating roulette. Mesagge:{ex.Message}, StackTrace:{ex.StackTrace}");

                return StatusCode(500, new { Error = Constants.SERVER_ERROR });
            }
        }
        [HttpPost("{rouletteId}/rounds")]
        public async Task<IActionResult> CreateRound(int rouletteId)
        {
            try
            {
                var roulette = await _unitOfWork.RouletteRepository.GetByIdAsync(rouletteId);

                if (roulette == null)
                    return NotFound(new { Message = Constants.ROULETTE_NOT_FOUND });

                var activeRound = await _unitOfWork.RoundRepository.GetActiveRoundByRouletteIdAsync(rouletteId);

                if (activeRound != null)
                    return Ok(new { Message = Constants.ROULETTE_ALREADY_OPEN });

                Round round = new Round() { RoulletteId = rouletteId, CreatedAt = DateTime.Now };

                await _unitOfWork.RoundRepository.AddAsync(round);

                roulette.Active = true;

                await _unitOfWork.CompleteAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating round. Mesagge:{ex.Message}, StackTrace:{ex.StackTrace}");

                return StatusCode(500, new { Error = Constants.SERVER_ERROR });
            }
        }

        [HttpPost("{rouletteId}/bets")]
        public async Task<IActionResult> PlaceBet(int rouletteId, [FromBody] Bet bet, [FromHeader] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (string.IsNullOrEmpty(userId))
                    return BadRequest(new { Message = Constants.USER_IVALID });

                var activeRound = await _unitOfWork.RoundRepository.GetActiveRoundByRouletteIdAsync(rouletteId);

                if (activeRound == null)
                    return BadRequest(new { Message = Constants.ROULETTE_NOT_FOUND_OR_ALREADY_CLOSED });

                if (bet.Amount > Constants.MAX_BET_AMMOUNT || bet.Amount < Constants.MIN_BET_AMMOUNT)
                    return BadRequest(new { Message = Constants.INVALID_BET_AMOUNT });

                if (bet.BetType == BetType.ColorBet) bet.Number = null;
                if (bet.BetType == BetType.NumberBet) bet.ColorBet = null;

                bet.RoundId = activeRound.Id;
                bet.User = userId;

                await _unitOfWork.BetRepository.AddAsync(bet);

                await _unitOfWork.CompleteAsync();

                return Ok(new { Message = Constants.BET_REGISTERED });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error placing bet. Mesagge:{ex.Message}, StackTrace:{ex.StackTrace}");

                return StatusCode(500, new { Error = Constants.SERVER_ERROR });
            }
        }

        [HttpGet("{rouletteId}/close")]
        public async Task<IActionResult> CloseRoulette(int rouletteId)
        {
            try
            {
                List<RoundResult> roundResults = new();

                var activeBetList = _unitOfWork.BetRepository.GetBetsByRoulleteId(new int[] { rouletteId }, true);

                if (!activeBetList.Any())
                    return NotFound(new { Message = Constants.ROULETTE_HAVE_NOT_BETS });

                var activeRound = await _unitOfWork.RoundRepository.GetActiveRoundByRouletteIdAsync(rouletteId);

                int winningNumber = RandomNumberGenerator.GetInt32(Constants.MIN_NUMBER_RANGE, Constants.MAX_NUMBER_RANGE + 1);

                var calculatedBetList = _earningsCalculator.CalculateEarnings(activeBetList, winningNumber);

                var closedRoulettes = await _unitOfWork.RouletteRepository.CloseRulettesAsync(new int[] { rouletteId });

                if (activeRound != null)
                    activeRound.ResultNumber = winningNumber;

                await _unitOfWork.CompleteAsync();

                roundResults = _unitOfWork.RoundRepository.GetRoundResultsByRoundId(activeRound.Id);

                return Ok(new { WinningNumber = winningNumber, WinningColor = Rules.ColorBetByNumber(winningNumber).ToString(), Results = roundResults });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error closing roulette. Mesagge:{ex.Message}, StackTrace:{ex.StackTrace}");

                return StatusCode(500, new { Error = Constants.SERVER_ERROR });
            }
        }
    }


}
