using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessMonitor.Model
{
    public static class ProcessFetcher
    {
        public static List<Process> Fetch()
        {
            return new List<Process>(Process.GetProcesses());
        }

        public static Process FetchByPid(string pid)
        {
            try
            {
                Process fetchedProcess = Process.GetProcessById(int.Parse(pid));
                return fetchedProcess;
            }
            catch
            {
                return null;
            }
        }
    }
}
