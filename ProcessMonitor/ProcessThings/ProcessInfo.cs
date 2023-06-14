using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.ProcessThings
{
    public class ProcessInfo
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
        public uint Priority => (uint)_process.GetPropertyValue("Priority");
        public DateTime CreationDate => ManagementDateTimeConverter.ToDateTime((string)_process.GetPropertyValue("CreationDate"));

        #endregion

        #region Methods

        public void Kill() { /* not implemented */ }

        internal void SetPriority(System.Diagnostics.ProcessPriorityClass priorityClass)
        {
            throw new NotImplementedException();
        }




        #endregion

        #region static members

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
        {
            return GetProcesses(p => pid.Equals(p["Handle"])).FirstOrDefault();
        }

        #endregion
    }
}
