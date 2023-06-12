using ProcessMonitor.Model.Presentation;
using System.Diagnostics;

namespace ProcessMonitor.Model.Logic
{
    public class ResourceMonitor
    {
        private readonly PerformanceCounter cpuCounter;
        private readonly PerformanceCounter ramPercentCounter;
        private readonly PerformanceCounter cacheHitsPercent;

        public ResourceMonitor()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramPercentCounter = new PerformanceCounter("Memory", "Available MBytes");
            cacheHitsPercent = new PerformanceCounter("Cache", "Copy Read Hits %");
        }

        public ResourcesUsage GetResourcesUsage()
        {
            return new ResourcesUsage
            {
                CpuUsage = cpuCounter.NextValue().ToString(),
                MemoryUsage = ramPercentCounter.NextValue().ToString(),
                CacheHits = cacheHitsPercent.NextValue().ToString()
            };
        }
    }
}
