using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;

namespace StackExchange.Profiling
{
    public static class MiniProfilerBlazorExtensions
    {
        public static void AddMiniProfiler(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MiniProfilerJsInterop>();
            var provider = new BlazorProfilerProvider();
            MiniProfiler.DefaultOptions.ProfilerProvider = provider;
            serviceCollection.AddSingleton(provider);

        }
    }

}
