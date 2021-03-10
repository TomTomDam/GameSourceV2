using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource;
using GameSource.Services.GameSource.Contracts;
using GameSource.Services.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
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
        readonly string AllowOrigin = "AllowOrigin";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowOrigin,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });

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
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IDeveloperService, DeveloperService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IPlatformService, PlatformService>();
            services.AddScoped<IPlatformTypeService, PlatformTypeService>();
            services.AddScoped<INewsArticleService, NewsArticleService>();
            services.AddScoped<INewsArticleCategoryService, NewsArticleCategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserStatusService, UserStatusService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUserProfileVisibilityService, UserProfileVisibilityService>();
            services.AddScoped<IUserProfileCommentService, UserProfileCommentService>();
            services.AddScoped<IUserProfileCommentPermissionService, UserProfileCommentPermissionService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IReviewCommentService, ReviewCommentService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(); //Must be placed between UseRouting and UseAuthentication
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{area:exists}/{controller=admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "GameSourceUser",
                    pattern: "{area:exists}/{controller=user}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
