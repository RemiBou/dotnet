using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using StackExchange.Profiling.Internal;

namespace StackExchange.Profiling
{
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
