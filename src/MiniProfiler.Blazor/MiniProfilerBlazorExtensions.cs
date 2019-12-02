using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Text.Json;
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

    public class MiniProfilerConverter : JsonConverter<MiniProfiler>
    {
        public override Category Read(ref Utf8JsonReader reader,
                                    Type typeToConvert,
                                    JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<MiniProfiler>(reader, options);
        }

        public override void Write(Utf8JsonWriter writer,
                                   MiniProfiler value,
                                   JsonSerializerOptions options)
        {
            var properties = value.GetType().Get
        }

    }

}
