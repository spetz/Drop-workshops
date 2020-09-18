using Drop.Core.Repositories;
using Drop.Infrastructure.Caching;
using Drop.Infrastructure.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Drop.Infrastructure
{
    public static class Extensions
    {
        // Extension method
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IParcelsRepository, InMemoryParcelsRepository>();
            services.AddMongo();
            
            return services;
        }
    }
}