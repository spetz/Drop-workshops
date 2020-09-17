using Drop.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Drop.Application
{
    public static class Extensions
    {
        // Extension method
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMessenger, Messenger>();
            services.AddScoped<IMessenger, MessengerV2>();

            return services;
        }
    }
}