using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            services.Configure<DatabaseSettings>(Configuration.GetSection("ConnectionStrings"));

            services.AddDbContext<GameSource_DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GameSource_DB")));
            services.AddDbContext<GameSourceUser_DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GameSourceUser_DB")));

            services.AddIdentity<User, UserRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<UserRole>()
                .AddEntityFrameworkStores<GameSourceUser_DBContext>();
            
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

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddScoped<IUserStatusRepository, UserStatusRepository>();
            services.AddScoped<IUserStatusService, UserStatusService>();
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
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            //SetUpDefaultRolesAndSuperUser(serviceProvider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{area:exists}/{controller=admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapToVueCliProxy(
                    "{*path}",
                    new SpaOptions { SourcePath = "src" },
                    npmScript: (System.Diagnostics.Debugger.IsAttached) ? "serve" : null,
                    regex: "Completed successfully",
                    forceKill: true
                    );

                endpoints.MapFallbackToController("Index", "Home");
            });

            app.UseSpa(spa => 
            {
                spa.Options.SourcePath = "src";
            });
        }

        private void SetUpDefaultRolesAndSuperUser(IServiceProvider serviceProvider)
        {
            //Initializing custom roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roleNames = { "Member", "Moderator", "Admin" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExists = roleManager.RoleExistsAsync(roleName).Result;
                if (!roleExists)
                {
                    //Create the roles and seed them to the database
                    roleResult = roleManager.CreateAsync(new UserRole { Name = roleName }).Result;
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
