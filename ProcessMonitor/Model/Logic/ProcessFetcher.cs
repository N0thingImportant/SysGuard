using ProcessMonitor.ProcessThings;
using System.Collections.Generic;

namespace ProcessMonitor.Model
{
    public static class ProcessFetcher
    {
        public static List<ProcessInfo> Fetch()
        {
            return new List<ProcessInfo>(ProcessInfo.GetProcesses());
        }

        public static ProcessInfo FetchByPid(string pid)
        {
            try
            {
                ProcessInfo fetchedProcess = ProcessInfo.GetProcessById(pid);
                return fetchedProcess;
            }
            catch
            {
                return null;
            }
        }
    }
}
