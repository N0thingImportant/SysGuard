﻿using Prism.Events;
using Prism.Mvvm;
using ProcessMonitor.Model;
using ProcessMonitor.Model.Events;
using ProcessMonitor.Model.Presentation;
using ProcessMonitor.ProcessThings;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProcessMonitor.ViewModels
{
    public class ProcessDetailsViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private ProcessInDetailsDisplay _displayedProcess = new ProcessInDetailsDisplay();
        public ProcessInDetailsDisplay DisplayedProcess
        {
            get { return _displayedProcess; }
            set { SetProperty(ref _displayedProcess, value); }
        }

        public ProcessDetailsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ProcessPidSelectedEvent>().Subscribe(LoadDetailsOfProcess);
        }

        private void LoadDetailsOfProcess(string pid)
        {
            ProcessInfo selectedProcess = ProcessInfo.GetProcessById(pid);
            if (selectedProcess == null)
            {
                return;
            }
            DisplayedProcess = MapProcessToDisplay(selectedProcess);
        }

        private ProcessInDetailsDisplay MapProcessToDisplay(ProcessInfo process)
        {
            List<string> childNames = new List<string>(process.GetChildProcesses().Select(ProcessInListDisplay.Create).Select(p =>p.ToString()));
            List<string> moduleNames = process.GetProcessModules();

            return new ProcessInDetailsDisplay
            {
                Name = process.Name,
                WorkingSet = (process.WorkingSetSize / 1024.0 / 1024.0),
                Pid = process.PID,
                ParentPid = process.ParentPID,
                CreationDate = process.CreationDate,
                Priority = process.Priority,
                CommandLine = process.CommandLine,
                ExecutablePath = process.ExecutablePath,
                ThreadsCount = process.ThreadCount,
                ModulesCount = process.HandleCount,
                ChildNames = childNames,
                ModuleNames = moduleNames,
            };
        }
    }
}
