using RoulettesGame.Data.Repositories;
using RoulettesGame.Models;

namespace RoulettesGame.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;


        public IRoulleteRepository RouletteRepository { get; }
        public IRoundRepository RoundRepository { get; }
        public IBetRepository BetRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            RouletteRepository = new RouletteRepository(_context);
            RoundRepository = new RoundRepository(_context);
            BetRepository = new BetRepository(_context);
        }

 
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
