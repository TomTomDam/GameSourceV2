using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Data
{
    public class GameSource_DBContext : DbContext
    {
        public GameSource_DBContext(DbContextOptions<GameSource_DBContext> options) : base(options)
        {

        }

        public DbSet<Game> Game { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Developer> Developer { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Platform> Platform { get; set; }
        public DbSet<NewsArticle> NewsArticle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}