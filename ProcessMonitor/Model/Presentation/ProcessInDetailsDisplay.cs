using System.Collections.Generic;

namespace ProcessMonitor.Model.Presentation
{
    public class ProcessInDetailsDisplay
    {
        public ProcessInDetailsDisplay()
        {
            Name = "-";
            Pid = "-";
            WorkingSet = "-";
            Priority = "-";
            ThreadsCount = "-";
            ModulesCount = "-";
            ParentPid = "-";
        }

        public string Name { get; set; }
        public string ExecutablePath { get; set; }
        public string CommandLine { get; set; }
        public string Pid { get; set; }
        public string WorkingSet { get; set; }
        public string Priority { get; set; }
        public string ThreadsCount { get; set; }
        public string ModulesCount { get; set; }
        public string ParentPid { get; set; }
        public string CreationDate { get; set; }
        public List<string> ChildNames { get; set; }
        public List<string> ModuleNames { get; set; }
    }
}
