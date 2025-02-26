using RoulettesGame.Data.Repositories;

namespace RoulettesGame.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRoulleteRepository RouletteRepository { get; }
        IRoundRepository RoundRepository { get; }
        IBetRepository BetRepository { get; }
        Task<int> CompleteAsync();
    }

}
