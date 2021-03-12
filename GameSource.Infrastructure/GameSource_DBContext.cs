using GameSource.Models.GameSource;
using GameSource.Models.GameSourceUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure
{
    public class GameSource_DBContext : IdentityDbContext<User, UserRole, int>
    {
        public GameSource_DBContext(DbContextOptions<GameSource_DBContext> options) : base(options)
        {

        }

        public DbSet<Game> Game { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Developer> Developer { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Platform> Platform { get; set; }
        public DbSet<PlatformType> PlatformType { get; set; }
        public DbSet<NewsArticle> NewsArticle { get; set; }
        public DbSet<NewsArticleCategory> NewsArticleCategory { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserStatus> UserStatus { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserProfileVisibility> UserProfileVisibility { get; set; }
        public DbSet<UserProfileComment> UserProfileComment { get; set; }
        public DbSet<UserProfileCommentPermission> UserProfileCommentPermission { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<ReviewComment> ReviewComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Change default delete behaviour from Cascade to Restrict.
            //Prevents multiple cascading paths - you must then explicitly update/delete a given row's foreign keys, afterwards you may update/delete the given row
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            modelBuilder.Entity<User>(e => e.ToTable(name: "User"));
            modelBuilder.Entity<UserRole>(e => e.ToTable(name: "UserRole"));
            modelBuilder.Entity<Review>().Property(p => p.Rating).HasColumnType("decimal(18,2)");

            //One PlatformType to Many Platforms
            modelBuilder.Entity<Platform>()
                .HasOne(p => p.PlatformType)
                .WithMany(p => p.Platforms)
                .HasForeignKey(p => p.PlatformTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PlatformType>()
                .HasMany(pt => pt.Platforms)
                .WithOne(pt => pt.PlatformType)
                .HasForeignKey(pt => pt.PlatformTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            //One NewsArticleCategory to Many NewsArticles
            modelBuilder.Entity<NewsArticle>()
                .HasOne(na => na.Category)
                .WithMany(na => na.NewsArticles)
                .HasForeignKey(na => na.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<NewsArticleCategory>()
                .HasMany(nac => nac.NewsArticles)
                .WithOne(nac => nac.Category)
                .HasForeignKey(nac => nac.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            //One UserProfileCommentPermission to Many UserProfiles
            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.UserProfileCommentPermission)
                .WithMany(up => up.UserProfile)
                .HasForeignKey(up => up.UserProfileCommentPermissionID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserProfileCommentPermission>()
                .HasMany(upcp => upcp.UserProfile)
                .WithOne(upcp => upcp.UserProfileCommentPermission)
                .HasForeignKey(upcp => upcp.UserProfileCommentPermissionID)
                .OnDelete(DeleteBehavior.Restrict);

            //One UserProfileVisibility to Many UserProfile
            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.UserProfileVisibility)
                .WithMany(up => up.UserProfile)
                .HasForeignKey(up => up.UserProfileVisibilityID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserProfileVisibility>()
                .HasMany(upv => upv.UserProfile)
                .WithOne(upv => upv.UserProfileVisibility)
                .HasForeignKey(upv => upv.UserProfileVisibilityID)
                .OnDelete(DeleteBehavior.Restrict);

            //Many ReviewComments to One Review
            modelBuilder.Entity<ReviewComment>()
                .HasOne(rc => rc.Review)
                .WithMany(rc => rc.ReviewComments)
                .HasForeignKey(rc => rc.Review)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Review>()
                .HasMany(r => r.ReviewComments)
                .WithOne(r => r.Review)
                .HasForeignKey(r => r.Review)
                .OnDelete(DeleteBehavior.Restrict);

            //Many UserProfileComments to One UserProfile
            modelBuilder.Entity<UserProfileComment>()
                .HasOne(upc => upc.UserProfile)
                .WithMany(upc => upc.Comments)
                .HasForeignKey(upc => upc.UserProfile)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserProfile>()
                .HasMany(upc => upc.Comments)
                .WithOne(upc => upc.UserProfile)
                .HasForeignKey(upc => upc.UserProfile)
                .OnDelete(DeleteBehavior.Restrict);

            //One Developer to Many Games
            modelBuilder.Entity<Developer>()
                .HasMany(d => d.Games)
                .WithOne(d => d.Developer)
                .HasForeignKey(d => d.DeveloperID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Developer)
                .WithMany(g => g.Games)
                .HasForeignKey(g => g.DeveloperID)
                .OnDelete(DeleteBehavior.Restrict);

            //One Genre to Many Games
            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Games)
                .WithOne(g => g.Genre)
                .HasForeignKey(g => g.GenreID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Genre)
                .WithMany(g => g.Games)
                .HasForeignKey(g => g.GenreID)
                .OnDelete(DeleteBehavior.Restrict);

            //One Platform to Many Games
            modelBuilder.Entity<Platform>()
                .HasMany(p => p.Games)
                .WithOne(p => p.Platform)
                .HasForeignKey(p => p.PlatformID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Platform)
                .WithMany(g => g.Games)
                .HasForeignKey(g => g.PlatformID)
                .OnDelete(DeleteBehavior.Restrict);

            //One Publisher to Many Games
            modelBuilder.Entity<Publisher>()
                .HasMany(p => p.Games)
                .WithOne(p => p.Publisher)
                .HasForeignKey(p => p.PublisherID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Publisher)
                .WithMany(g => g.Games)
                .HasForeignKey(g => g.PublisherID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
