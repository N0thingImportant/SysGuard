using ProcessMonitor.ProcessThings;

namespace ProcessMonitor.Model
{
    public class ProcessInListDisplay
    {
        public string PID { get; set; }
        public string Name { get; set; }

        public string DisplayName
        {
            get
            {
                return $"[{PID,6}]: {Name}";
            }
        }

        public static ProcessInListDisplay Create(ProcessInfo process)
        {
            return new ProcessInListDisplay { Name = process.Name, PID = process.PID.ToString() };
        }

        public override string ToString() => DisplayName;

        public override bool Equals(object obj)
        {
            if (obj is ProcessInListDisplay process)
            {
                return process.PID == PID;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return PID.GetHashCode();
        }
    }
}
