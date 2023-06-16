using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessMonitor.Model.Presentation
{
    public class ProcessInDetailsDisplay
    {
        public string Name { get; set; }
        public string ExecutablePath { get; set; }
        public string CommandLine { get; set; }
        public uint? Pid { get; set; }
        public double? WorkingSet { get; set; }
        public ProcessPriorityClass? Priority { get; set; }
        public uint? ThreadsCount { get; set; }
        public uint? ModulesCount { get; set; }
        public uint? ParentPid { get; set; }
        public DateTime? CreationDate { get; set; }
        public List<string> ChildNames { get; set; }
        public List<string> ModuleNames { get; set; }
    }
}
