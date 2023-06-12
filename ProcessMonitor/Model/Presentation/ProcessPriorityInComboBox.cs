using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessMonitor.Model.Presentation
{
    public class ProcessPriorityInComboBox
    {
        public ProcessPriorityClass Priority { get; set; }
        public string PriorityName { get; set; }
    }

    public static class ProcessPriorityInComboBoxGenerator
    {
        public static List<ProcessPriorityInComboBox> Generate()
        {
            List<ProcessPriorityInComboBox> prioritaits = new List<ProcessPriorityInComboBox>
            {
                new ProcessPriorityInComboBox { Priority = ProcessPriorityClass.Idle, PriorityName = "Idle" },
                new ProcessPriorityInComboBox { Priority = ProcessPriorityClass.BelowNormal, PriorityName = "Below normal" },
                new ProcessPriorityInComboBox { Priority = ProcessPriorityClass.Normal, PriorityName = "Normal" },
                new ProcessPriorityInComboBox { Priority = ProcessPriorityClass.AboveNormal, PriorityName = "Above normal" },
                new ProcessPriorityInComboBox { Priority = ProcessPriorityClass.RealTime, PriorityName = "Real time" }
            };
            return prioritaits;
        }
    }
}
