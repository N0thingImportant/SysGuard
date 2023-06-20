using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;

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

        private const int maxBuffer = 32;
        private const int timer = 666;

        private static Thread updater;

        private static List<Dictionary<uint, ProcessInfo>> buffer = new List<Dictionary<uint, ProcessInfo>>(maxBuffer);

        static ProcessInfo()
        {
            AddProcessesDic();
            updater = new Thread(UpdateProcessBuffer);
            updater.Start();
        }

        private static void UpdateProcessBuffer()
        {
            do
            {
                if (buffer.Count == maxBuffer)
                    buffer.RemoveAt(0);
                AddProcessesDic();
                Thread.Sleep(timer);
            }
            while (true);
        }

        private static void AddProcessesDic()
        {
            var dic = new Dictionary<uint, ProcessInfo>();
            foreach (var proc in GetProcessesInternal())
            {
                dic[proc.PID] = proc;
            }
            lock (buffer)
                buffer.Add(dic);
        }

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

        internal static IEnumerable<ProcessInfo> GetProcesses()
        {
            lock (buffer)
                return buffer[buffer.Count - 1].Values.ToArray();
        }

        internal static IEnumerable<ProcessInfo> GetProcessesInternal()
        {
            return searcher.Get().Cast<ManagementObject>().Select(Create);
        }

        internal static ProcessInfo GetProcessById(string pid)
        {
            return GetProcessById(uint.Parse(pid));
        }

        internal static ProcessInfo GetProcessById(uint pid)
        {
            var list = buffer.FirstOrDefault(l => l.ContainsKey(pid));
            if (list == null)
                return null;
            return list[pid];
        }

        #endregion
    }
}
