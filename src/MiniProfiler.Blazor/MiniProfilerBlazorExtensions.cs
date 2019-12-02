using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.JSInterop;
using System.Threading.Tasks;
namespace StackExchange.Profiling
{
    public static class MiniProfilerBlazorExtensions
    {
        public static void AddMiniProfiler(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MiniProfilerJsInterop>();
        }
    }

    public class MiniProfilerJsInterop
    {
        private IJSRuntime jsRuntime;

        public MiniProfilerJsInterop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }
        public async Task RenderProfiler(MiniProfiler profiler)
        {
            await jsRuntime.InvokeVoidAsync("MiniProfiler.renderProfiler", profiler);
        }

    }

}
