using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Profiling.Internal;
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

    public class MiniProfilerBlazorOptions : MiniProfilerBaseOptions
    {
        /// <summary>
        /// The path under which ALL routes are registered in, defaults to the application root.  For example, "/myDirectory/" would yield
        /// "/myDirectory/includes.min.js" rather than "/mini-profiler-resources/includes.min.js"
        /// Any setting here should be absolute for the application, e.g. "/myDirectory/"
        /// </summary>
        public string RouteBasePath { get; set; } = "/mini-profiler-resources";

    }

}
