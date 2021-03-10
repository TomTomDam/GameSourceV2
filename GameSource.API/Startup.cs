using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GameSource.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Databases
            services.AddDbContext<GameSource_DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GameSource_DB")));
            //ASP.NET Identity Core
            services.AddIdentity<User, UserRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<UserRole>()
                .AddEntityFrameworkStores<GameSource_DBContext>();

            //Services
            services.AddScoped<IGameService, GameService>();
            //services.AddScoped<IGenreService, GenreService>();
            //services.AddScoped<IDeveloperService, DeveloperService>();
            //services.AddScoped<IPublisherService, PublisherService>();
            //services.AddScoped<IPlatformService, PlatformService>();
            //services.AddScoped<IPlatformTypeService, PlatformTypeService>();
            //services.AddScoped<INewsArticleService, NewsArticleService>();
            //services.AddScoped<INewsArticleCategoryService, NewsArticleCategoryService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUserRoleService, UserRoleService>();
            //services.AddScoped<IUserStatusService, UserStatusService>();
            //services.AddScoped<IUserProfileService, UserProfileService>();
            //services.AddScoped<IUserProfileVisibilityService, UserProfileVisibilityService>();
            //services.AddScoped<IUserProfileCommentService, UserProfileCommentService>();
            //services.AddScoped<IUserProfileCommentPermissionService, UserProfileCommentPermissionService>();
            //services.AddScoped<IReviewService, ReviewService>();
            //services.AddScoped<IReviewCommentService, ReviewCommentService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
