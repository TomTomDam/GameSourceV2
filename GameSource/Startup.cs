using System;
using GameSource.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Data.Repositories.GameSource;
using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Data.Repositories.GameSourceUser;
using GameSource.Services.GameSource.Contracts;
using GameSource.Services.GameSource;
using GameSource.Services.GameSourceUser.Contracts;
using GameSource.Services.GameSourceUser;
using GameSource.Models.GameSourceUser;
using Microsoft.AspNetCore.Identity;
using GameSource.Data.Settings;
using VueCliMiddleware;
using Microsoft.AspNetCore.SpaServices;
using System.Threading.Tasks;

namespace GameSource
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSpaStaticFiles(option => option.RootPath = "wwwroot/vue");
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            //Policy-based authorization
            services.AddAuthorization(options => 
            {
                options.AddPolicy("CreateGenrePolicy",
                    policy => policy.RequireClaim("Create Genre"));
            });

            //Databases
            services.Configure<DatabaseSettings>(Configuration.GetSection("ConnectionStrings"));
            services.AddDbContext<GameSource_DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GameSource_DB")));

            //Configure ASP.NET Identity Core
            services.AddIdentity<User, UserRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<UserRole>()
                .AddEntityFrameworkStores<GameSource_DBContext>();
            
            //Services and their respective Repositories
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameService, GameService>();

            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();

            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IDeveloperService, DeveloperService>();

            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IPublisherService, PublisherService>();

            services.AddScoped<IPlatformRepository, PlatformRepository>();
            services.AddScoped<IPlatformService, PlatformService>();

            services.AddScoped<IPlatformTypeRepository, PlatformTypeRepository>();
            services.AddScoped<IPlatformTypeService, PlatformTypeService>();

            services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            services.AddScoped<INewsArticleService, NewsArticleService>();

            services.AddScoped<INewsArticleCategoryRepository, NewsArticleCategoryRepository>();
            services.AddScoped<INewsArticleCategoryService, NewsArticleCategoryService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddScoped<IUserStatusRepository, UserStatusRepository>();
            services.AddScoped<IUserStatusService, UserStatusService>();

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddScoped<IUserProfileVisibilityRepository, UserProfileVisibilityRepository>();
            services.AddScoped<IUserProfileVisibilityService, UserProfileVisibilityService>();

            services.AddScoped<IUserProfileCommentRepository, UserProfileCommentRepository>();
            services.AddScoped<IUserProfileCommentService, UserProfileCommentService>();

            services.AddScoped<IUserProfileCommentPermissionRepository, UserProfileCommentPermissionRepository>();
            services.AddScoped<IUserProfileCommentPermissionService, UserProfileCommentPermissionService>();

            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();

            services.AddScoped<IReviewCommentRepository, ReviewCommentRepository>();
            services.AddScoped<IReviewCommentService, ReviewCommentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //SetUpDefaultUserAttributesAndSuperUser(serviceProvider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{area:exists}/{controller=admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "GameSourceUser",
                    pattern: "{area:exists}/{controller=user}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapToVueCliProxy(
                //    "{*path}",
                //    new SpaOptions { SourcePath = "src" },
                //    npmScript: (System.Diagnostics.Debugger.IsAttached) ? "serve" : null,
                //    regex: "Completed successfully",
                //    forceKill: true);

                endpoints.MapRazorPages();
            });

            //app.UseSpa(spa =>
            //{
            //    spa.Options.SourcePath = "src";
            //});
        }

        private void SetUpDefaultUserAttributesAndSuperUser(IServiceProvider serviceProvider)
        {
            //Initializing custom roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var userStatusService = serviceProvider.GetRequiredService<UserStatusService>();
            string[] roleNames = { "Member", "Moderator", "Admin" };
            string[] statusNames = { "Active", "Deactivated" };
            IdentityResult roleResult;
            Task statusResult;

            foreach (var roleName in roleNames)
            {
                var roleExists = roleManager.RoleExistsAsync(roleName).Result;
                if (!roleExists)
                {
                    //Create the roles and seed them to the database
                    roleResult = roleManager.CreateAsync(new UserRole { Name = roleName }).Result;
                }
            }

            foreach (var statusName in statusNames)
            {
                var statusExists = userStatusService.GetByNameAsync(statusName);
                if (statusExists != null)
                {
                    statusResult = userStatusService.InsertAsync(new UserStatus { Name = statusName });
                }
            }

            //Create a "super user" that has full access to the application
            var superUser = new User
            {
                UserName = "thomasdam",
                Email = "thomasdam@live.co.uk",
            };

            //Config values are provided in appsettings.json
            string userPassword = "hellothere1";
            var user = userManager.FindByEmailAsync("thomasdam@live.co.uk").Result;

            if (user == null)
            {
                var createSuperUser = userManager.CreateAsync(superUser, userPassword).Result;
                if (createSuperUser.Succeeded)
                {
                    //Add Admin role to the super user
                    userManager.AddToRoleAsync(superUser, "Admin");
                }
            }
        }
    }
}
