using GameSource.Models;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure
{
    public class GameSource_DBContext : DbContext
    {
        public GameSource_DBContext(DbContextOptions<GameSource_DBContext> options) : base(options)
        {

        }

        public DbSet<Game> Game { get; set; }
    }
}
