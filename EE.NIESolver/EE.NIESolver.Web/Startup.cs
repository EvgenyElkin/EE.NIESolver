using System;
using System.Reflection;
using EE.NIESolver.DataLayer;
using EE.NIESolver.DataLayer.Constants;
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

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var conntectionString = Configuration.GetConnectionString("Server");
            var migrationAssembly = Assembly.GetExecutingAssembly().FullName;
            services
                .AddDbContext<SolverContext>(options =>
                {
                    options.UseNpgsql(conntectionString, opt => opt.MigrationsAssembly(migrationAssembly));
                })
                .AddScoped<IConstantService, ConstantService>()
                .WithDataLayer()
                .AddMvc();

            var serviceProvider = services.BuildServiceProvider();
            InitializeStaticContexts(serviceProvider);
            return serviceProvider;
        }

        public void InitializeStaticContexts(IServiceProvider provider)
        {
            ConstantConext.SetFactory(provider.GetService<IConstantService>);
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
