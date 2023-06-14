using ProcessMonitor.ProcessThings;
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
            ProcessInfo matchingProcess = ProcessFetcher.FetchByPid(pid);
            if (matchingProcess != null)
            {
                matchingProcess.Kill();
            }
        }

        public static void SetPriority(string pid, ProcessPriorityClass priorityClass)
        {
            ProcessInfo matchingProcess = ProcessFetcher.FetchByPid(pid);
            if (matchingProcess != null)
            {
                matchingProcess.SetPriority(priorityClass);
            }
        }
    }
}
