using System.Diagnostics;

namespace ProcessMonitor.Model.Logic
{
    public static class ProcessManager
    {
        public static Process StartProcess(int msDuration)
        {
            Process newProcess = Process.Start("notepad.exe");
            return newProcess;
        }

        public static void KillProcess(string pid)
        {
            Process matchingProcess = ProcessFetcher.FetchByPid(pid);
            if (matchingProcess != null)
            {
                matchingProcess.Kill();
            }
        }

        public static void SetPriority(string pid, ProcessPriorityClass priorityClass)
        {
            Process matchingProcess = ProcessFetcher.FetchByPid(pid);
            if (matchingProcess != null)
            {
                matchingProcess.PriorityClass = priorityClass;
            }
        }
    }
}
