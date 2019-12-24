using Microsoft.AspNetCore.Components;

namespace StackExchange.Profiling
{
    public class MiniProfilerComponentBase : ComponentBase
    {
        private Timing _currentTiming;

        protected override bool ShouldRender()
        {
            _currentTiming = MiniProfiler.Current.Step("Rendering " + this.GetType().Name);
            return true;
        }
        protected override void OnAfterRender(bool firstRender)
        {
            _currentTiming?.Stop();
        }


    }

}
