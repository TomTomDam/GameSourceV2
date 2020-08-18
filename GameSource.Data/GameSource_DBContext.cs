using GameSource.Models.GameSource;
using GameSource.Models.GameSourceUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Data
{
    public class GameSource_DBContext : IdentityDbContext<User, UserRole, int>
    {
        public GameSource_DBContext(DbContextOptions<GameSource_DBContext> options) 
            : base(options)
        {

        }

        public DbSet<Game> Game { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Developer> Developer { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Platform> Platform { get; set; }
        public DbSet<PlatformType> PlatformType { get; set; }
        public DbSet<NewsArticle> NewsArticle { get; set; }
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

            modelBuilder.Entity<User>(e => e.ToTable(name: "User"));
            modelBuilder.Entity<UserRole>(e => e.ToTable(name: "UserRole"));
        }
    }
}