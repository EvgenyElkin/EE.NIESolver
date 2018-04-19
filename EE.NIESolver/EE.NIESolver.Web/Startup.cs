using System.Reflection;
using EE.NIESolver.DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EE.NIESolver.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var conntectionString = Configuration.GetConnectionString("Server");
            var migrationAssembly = Assembly.GetExecutingAssembly().FullName;
            services
                .AddDbContext<SolverContext>(options =>
                {
                    options.UseNpgsql(conntectionString, opt => opt.MigrationsAssembly(migrationAssembly));
                })
                .WithDataLayer()
                .AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
