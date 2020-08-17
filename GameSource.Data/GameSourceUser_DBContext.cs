using GameSource.Models.GameSourceUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Data
{
    public class GameSourceUser_DBContext : IdentityDbContext<User, UserRole, int>
    {
        public GameSourceUser_DBContext(DbContextOptions<GameSourceUser_DBContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
