using Microsoft.EntityFrameworkCore;
using RealEstate.API.Models;

namespace RealEstate.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            //string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

            Configuration = builder.Build();

        }
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddDbContext<RealEstateContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("RealEstateConnectionstring")));
                services.AddControllers();
            }
            if (_env.IsStaging())
            {
                services.AddDbContext<RealEstateContext>(option =>
                option.UseSqlServer(Environment.GetEnvironmentVariable("RealEstateConnectionstring")));
                services.AddControllers();
            }

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
