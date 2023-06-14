using ProcessMonitor.ProcessThings;

namespace ProcessMonitor.Model
{
    public class ProcessInListDisplay
    {
        public string Pid { get; set; }
        public string Name { get; set; }

        public string DisplayName
        {
            get
            {
                return $"[{Pid,6}]: {Name}";
            }
        }

        public static ProcessInListDisplay Create(ProcessInfo process)
        {
            return new ProcessInListDisplay { Name = process.Name, Pid = process.PID.ToString() };
        }

        public override string ToString() => DisplayName;

        public override bool Equals(object obj)
        {
            if (obj is ProcessInListDisplay process)
            {
                return process.Pid == Pid;
            }
            return false;
        }
    }
}
