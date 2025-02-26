using Microsoft.EntityFrameworkCore;
using RoulettesGame.Models;

namespace RoulettesGame.Data.Repositories
{
    public class RoundRepository : Repository<Round>, IRoundRepository
    {
        public RoundRepository(ApplicationDbContext context) : base(context) { }

    }
}
