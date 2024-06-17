using Library.Helpers;
using Library.IServices;
using Library.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Mímir")]
namespace Library
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection AddLibrary(this IServiceCollection services)
        {
            // yo kaile runchuncha??

            services.AddScoped<ITestService,TestService>();
            services.AddScoped<JsonReader>();
            return services;
        }
    }
}
