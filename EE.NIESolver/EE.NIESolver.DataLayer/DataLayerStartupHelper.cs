using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EE.NIESolver.DataLayer
{
    public static class DataLayerStartupHelper
    {
        public static IServiceCollection WithDataLayer(this IServiceCollection services)
        {
            services.AddScoped<IDataRepository, DataRepository>()
                .AddScoped<IConstantService, ConstantService>();

            return services;
        }
    }
}