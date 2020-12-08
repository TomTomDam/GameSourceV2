using GameSource.Data;
using GameSource.Data.Repositories.GameSource;
using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Data.Repositories.GameSourceUser;
using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Data.Settings;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowOrigin,
                    builder => {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });

            services.Configure<DatabaseSettings>(Configuration.GetSection("ConnectionStrings"));
            services.AddDbContext<GameSource_DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GameSource_DB")));

            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();

            services.AddScoped<IPlatformRepository, PlatformRepository>();
            services.AddScoped<IPlatformService, PlatformService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowOrigin);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
