using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Text.Json;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components;
using StackExchange.Profiling.Internal;

namespace StackExchange.Profiling
{
    public static class MiniProfilerBlazorExtensions
    {
        public static void AddMiniProfiler(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MiniProfilerJsInterop>();
            var provider = new BlazorProfilerProvider();
            MiniProfiler.DefaultOptions.ProfilerProvider = provider;
            serviceCollection.AddSingleton<BlazorProfilerProvider>(provider);
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
            await jsRuntime.InvokeVoidAsync("MiniProfiler.buttonShow", new SerializableMiniProfiler
            {
                Id = profiler.Id,
                Name = profiler.Name,
                Started = profiler.Started,
                DurationMilliseconds = profiler.DurationMilliseconds,
                MachineName = profiler.MachineName,
                CustomLinks = profiler.CustomLinks,
                Root = MapToSerializable(profiler.Root),
                ClientTimings = profiler.ClientTimings != null ? new SerializableCLientTimings
                {
                    RedirectCount = profiler.ClientTimings.RedirectCount,
                    Timings = profiler.ClientTimings.Timings.Select(t => new SerializableClientTiming
                    {
                        Name = t.Name,
                        Start = t.Start,
                        Duration = t.Duration

                    })
                    .ToList()
                } : null,
                User = profiler.User,
                HasUserViewed = profiler.HasUserViewed
            });
        }


        private SerializableTiming MapToSerializable(Timing timing)
        {
            return new SerializableTiming
            {
                Id = timing.Id,
                Name = timing.Name,
                DurationMilliseconds = timing.DurationMilliseconds,
                StartMilliseconds = timing.StartMilliseconds,
                Children = timing.Children?.Select(t => MapToSerializable(t))?.ToList(),
                CustomTimings = timing.CustomTimings?.ToDictionary(
                    kvp => kvp.Key,
                    kvp =>
                        kvp.Value.Select(
                            c => new SerializableCustomTiming
                            {
                                Id = c.Id,
                                CommandString = c.CommandString,
                                ExecuteType = c.ExecuteType,
                                StackTraceSnippet = c.StackTraceSnippet,
                                StartMilliseconds = c.StartMilliseconds,
                                DurationMilliseconds = c.DurationMilliseconds,
                                Errored = c.Errored,
                                FirstFetchDurationMilliseconds = c.FirstFetchDurationMilliseconds
                            }
                    ).ToList()
                )
            };
        }

        private class SerializableMiniProfiler
        {

            [JsonPropertyName("Id")] public Guid Id { get; set; }
            [JsonPropertyName("Name")] public string Name { get; set; }
            [JsonPropertyName("Started")] public DateTime Started { get; set; }
            [JsonPropertyName("DurationMilliseconds")] public decimal DurationMilliseconds { get; set; }
            [JsonPropertyName("MachineName")] public string MachineName { get; set; }
            [JsonPropertyName("CustomLinks")] public Dictionary<string, string> CustomLinks { get; set; }
            [JsonPropertyName("Root")] public SerializableTiming Root { get; set; }
            [JsonPropertyName("ClientTimings")] public SerializableCLientTimings ClientTimings { get; set; }
            [JsonPropertyName("User")] public string User { get; set; }
            [JsonPropertyName("HasUserViewed")] public bool HasUserViewed { get; set; }
        }

        private class SerializableTiming
        {
            [JsonPropertyName("Id")] public Guid Id { get; set; }
            [JsonPropertyName("Name")] public string Name { get; set; }
            [JsonPropertyName("DurationMilliseconds")] public decimal? DurationMilliseconds { get; set; }
            [JsonPropertyName("StartMilliseconds")] public decimal StartMilliseconds { get; set; }
            [JsonPropertyName("Children")] public List<SerializableTiming> Children { get; set; }
            [JsonPropertyName("CustomTimings")] public Dictionary<string, List<SerializableCustomTiming>> CustomTimings { get; set; }
        }

        private class SerializableCustomTiming
        {
            [JsonPropertyName("Id")] public Guid Id { get; set; }
            [JsonPropertyName("CommandString")] public string CommandString { get; set; }
            [JsonPropertyName("ExecuteType")] public string ExecuteType { get; set; }
            [JsonPropertyName("StackTraceSnippet")] public string StackTraceSnippet { get; set; }
            [JsonPropertyName("StartMilliseconds")] public decimal StartMilliseconds { get; set; }
            [JsonPropertyName("DurationMilliseconds")] public decimal? DurationMilliseconds { get; set; }
            [JsonPropertyName("Errored")] public bool Errored { get; set; }
            [JsonPropertyName("FirstFetchDurationMilliseconds")] public decimal? FirstFetchDurationMilliseconds { get; set; }
        }

        private class SerializableCLientTimings
        {
            [JsonPropertyName("RedirectCount")] public int RedirectCount { get; set; }
            [JsonPropertyName("Timings")] public List<SerializableClientTiming> Timings { get; set; }
        }

        private class SerializableClientTiming
        {
            [JsonPropertyName("Name")] public string Name { get; set; }
            [JsonPropertyName("Start")] public decimal Start { get; set; }
            [JsonPropertyName("Duration")] public decimal Duration { get; set; }
        }
    }


    internal class BlazorProfilerProvider : IAsyncProfilerProvider
    {

        private List<MiniProfiler> _unhandledProfilers = new List<MiniProfiler>();

        public MiniProfiler CurrentProfiler { get; private set; }

        private event EventHandler<MiniProfiler> _OnProfilerStopped;
        public event EventHandler<MiniProfiler> OnProfilerStopped
        {
            add
            {
                _OnProfilerStopped += value;
                foreach (var profiler in _unhandledProfilers)
                {
                    _OnProfilerStopped.Invoke(this, profiler);
                }
                _unhandledProfilers.Clear();
            }
            remove
            {
                _OnProfilerStopped -= value;
            }
        }
        public void Stopped(MiniProfiler profiler, bool discardResults)
        {
            Console.WriteLine("Stopped");
            if (profiler == CurrentProfiler)
                CurrentProfiler = null;
            if (!discardResults)
                RaiseProfilerStoppedEvent(profiler, discardResults);
        }

        private void RaiseProfilerStoppedEvent(MiniProfiler profiler, bool discardResults)
        {
            if (!discardResults)
            {
                if (_OnProfilerStopped != null && _OnProfilerStopped.GetInvocationList() != null)
                {
                    _OnProfilerStopped.Invoke(this, profiler);
                }
                else
                {
                    _unhandledProfilers.Add(profiler);
                }
            }
        }

        public async Task StoppedAsync(MiniProfiler profiler, bool discardResults)
        {
            Console.WriteLine("StoppedAsync");

            if (profiler == CurrentProfiler)
                CurrentProfiler = null;

            if (!discardResults)
            {
                RaiseProfilerStoppedEvent(profiler, discardResults);
            }
        }

        public MiniProfiler Start(string profilerName, MiniProfilerBaseOptions options)
        {
            CurrentProfiler = new MiniProfiler(profilerName ?? "MiniProfiler", options);
            return CurrentProfiler;
        }
    }


}
