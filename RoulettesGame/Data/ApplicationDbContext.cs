using Microsoft.EntityFrameworkCore;
using RoulettesGame.Models;

namespace RoulettesGame.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Roulette> Roullete { get; set; }
        public DbSet<Round> Round { get; set; }
        public DbSet<Bet> Bet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roulette>().ToTable("Roulettes");
            modelBuilder.Entity<Round>().ToTable("Rounds");
            modelBuilder.Entity<Bet>().ToTable("Bets");
        }

    }
}
