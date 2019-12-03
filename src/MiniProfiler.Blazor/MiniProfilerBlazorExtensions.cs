using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Text.Json;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using System.Linq;
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
            await jsRuntime.InvokeVoidAsync("MiniProfiler.renderProfiler", new
            {
                profiler.Id,
                profiler.Name,
                profiler.Started,
                profiler.DurationMilliseconds,
                profiler.MachineName,
                profiler.CustomLinks,
                Root = MapToSerializable(profiler.Root),
                profiler.ClientTimings,
                profiler.User,
                profiler.HasUserViewed
            });
        }


        public object MapToSerializable(Timing timing)
        {
            return new
            {
                timing.Id,
                timing.Name,
                timing.DurationMilliseconds,
                timing.StartMilliseconds,
                Children = timing.Children?.Select(t => MapToSerializable(t))?.ToList(),
                timing.CustomTimings
            };
        }

    }


}
