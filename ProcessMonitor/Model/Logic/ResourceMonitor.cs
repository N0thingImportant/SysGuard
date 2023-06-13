using ProcessMonitor.Model.Presentation;
using System.Diagnostics;

namespace ProcessMonitor.Model.Logic
{
    public class ResourceMonitor
    {
        private readonly PerformanceCounter cpuCounter;
        private readonly PerformanceCounter ramPercentCounter;
        private readonly PerformanceCounter ramAvailableCounter;

        public ResourceMonitor()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramPercentCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            ramAvailableCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public ResourcesUsage GetResourcesUsage()
        {
            return new ResourcesUsage
            {
                CpuUsage = cpuCounter.NextValue().ToString("0.00'%'"),
                MemoryUsage = ramPercentCounter.NextValue().ToString("0.00'%'"),
                MemoryAvail = ramAvailableCounter.NextValue().ToString("#,0.00 Mb"),
            };
        }
    }
}
