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
