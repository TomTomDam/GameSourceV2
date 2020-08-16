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

        public DbSet<User> User { get; set; }
        public DbSet<UserStatus> UserStatus { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserProfileVisibility> UserProfileVisibility { get; set; }
        public DbSet<UserProfileComment> UserProfileComment { get; set; }
        public DbSet<UserProfileCommentPermission> UserProfileCommentPermission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
