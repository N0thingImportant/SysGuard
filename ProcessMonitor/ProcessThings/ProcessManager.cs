using System.Diagnostics;

namespace ProcessMonitor.ProcessThings
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
            ProcessInfo matchingProcess = ProcessInfo.GetProcessById(pid);
            if (matchingProcess != null)
            {
                matchingProcess.Kill();
            }
        }

        public static void SetPriority(string pid, ProcessPriorityClass priorityClass)
        {
            ProcessInfo matchingProcess = ProcessInfo.GetProcessById(pid);
            if (matchingProcess != null)
            {
                matchingProcess.SetPriority(priorityClass);
            }
        }
    }
}
