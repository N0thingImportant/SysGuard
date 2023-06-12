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
                CpuUsage = (cpuCounter.NextValue()/100).ToString("P2"),
                MemoryUsage = ramPercentCounter.NextValue().ToString("0.00 Mb"),
                CacheHits = (cacheHitsPercent.NextValue()/100).ToString("P2")
            };
        }
    }
}
