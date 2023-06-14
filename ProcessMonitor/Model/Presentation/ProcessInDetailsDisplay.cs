﻿using System.Collections.Generic;

namespace ProcessMonitor.Model.Presentation
{
    public class ProcessInDetailsDisplay
    {
        public ProcessInDetailsDisplay()
        {
            Name = "NAME";
            Pid = "-";
            Memory = "MEMORY";
            Priority = "-";
            ThreadsCount = "-";
            ModulesCount = "-";
            Responding = "-";
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public string Args { get; set; }
        public string Pid { get; set; }
        public string Memory { get; set; }
        public string Priority { get; set; }
        public string ThreadsCount { get; set; }
        public string ModulesCount { get; set; }
        public string Responding { get; set; }
        public List<string> ThreadNames { get; set; }
        public List<string> ModuleNames { get; set; }
    }
}
