using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace ProcessMonitor.ProcessThings
{
    public class ProcessInfo : IDisposable
    {
        private ManagementObject _process;

        internal ProcessInfo(ManagementObject process)
        {
            if (process.ClassPath.ClassName != "Win32_Process")
            {
                throw new InvalidOperationException("Cannot create process info");
            }
            _process = process;
        }

        #region Properties

        public uint PID => (uint)_process.GetPropertyValue("ProcessId");
        public uint ParentPID => (uint)_process.GetPropertyValue("ParentProcessId");
        public string Name => (string)_process.GetPropertyValue("Name");
        public string CommandLine => (string)_process.GetPropertyValue("CommandLine");
        public string ExecutablePath => (string)_process.GetPropertyValue("ExecutablePath");
        public uint ThreadCount => (uint)_process.GetPropertyValue("ThreadCount");
        public uint HandleCount => (uint)_process.GetPropertyValue("HandleCount");
        public ulong WorkingSetSize => (ulong)_process.GetPropertyValue("WorkingSetSize");
        public ProcessPriorityClass Priority => ConvertPriority((uint)_process.GetPropertyValue("Priority"));
        public DateTime CreationDate => ManagementDateTimeConverter.ToDateTime((string)_process.GetPropertyValue("CreationDate"));

        #endregion

        #region Methods

        public void Kill()
        {
            _process.InvokeMethod("Terminate", null);
        }

        public void SetPriority(ProcessPriorityClass priorityClass)
        {
            _process.InvokeMethod("SetPriority", new object[] { priorityClass });
        }

        public IEnumerable<ProcessInfo> GetChildProcesses()
        {
            var searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ParentProcessId={PID}");
            return searcher.Get().Cast<ManagementObject>().Select(Create);
        }

        public List<string> GetProcessModules()
        {
            // It's not a good practic but work
            try
            {
                var process = Process.GetProcessById((int)PID);
                var modules = process.Modules;
                var result = new List<string>(modules.Count);
                for (int i = 0; i < modules.Count; ++i)
                    result.Add(modules[i].ModuleName);
                return result;
            }
            catch
            {
                return new List<string> { };
            }
        }

        void IDisposable.Dispose()
        {
            _process.Dispose();
        }

        #endregion

        #region static members

        private static ProcessPriorityClass ConvertPriority(uint value)
        {
            switch (value)
            {
                case 0: return 0;
                case 4: return ProcessPriorityClass.Idle;
                case 6: return ProcessPriorityClass.BelowNormal;
                case 8: return ProcessPriorityClass.Normal;
                case 10: return ProcessPriorityClass.AboveNormal;
                case 13: return ProcessPriorityClass.High;
                case 24: return ProcessPriorityClass.RealTime;
            }
            return ProcessPriorityClass.Normal;
        }

        private static ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process");

        private static ProcessInfo Create(ManagementObject obj) => new ProcessInfo(obj);

        private static bool AllProcesses(ManagementObject obj) => true;

        internal static IEnumerable<ProcessInfo> GetProcesses(Func<ManagementObject, bool> predicate)
        {
            return searcher.Get().Cast<ManagementObject>().Where(predicate).Select(Create);
        }

        internal static IEnumerable<ProcessInfo> GetProcesses()
        {
            return GetProcesses(AllProcesses);
        }

        internal static ProcessInfo GetProcessById(string pid)
        { // TODO!
            return GetProcesses(p => pid.Equals(p["Handle"])).FirstOrDefault();
        }

        internal static ProcessInfo GetProcessById(uint pid)
        {
            return GetProcesses(p => pid.Equals(p["Handle"])).FirstOrDefault();
        }

        #endregion
    }
}
