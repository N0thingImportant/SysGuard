using ProcessMonitor.DB;
using ProcessMonitor.ProcessThings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor
{
    public class Monitor
    {
        static private ManagementEventWatcher startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
        static private ManagementEventWatcher stopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));

        public static bool Started { get; private set; }

        public static void Start()
        {
            if (Started)
                return;

            startWatch.EventArrived += StartWatch_EventArrived;
            startWatch.Start();

            stopWatch.EventArrived += StopWatch_EventArrived;
            stopWatch.Start();

            Started = true;
        }

        public static void Stop()
        {
            if (!Started)
                return;

            startWatch.Stop();
            startWatch.EventArrived -= StartWatch_EventArrived;

            stopWatch.Stop();
            stopWatch.EventArrived -= StopWatch_EventArrived;

            Started = false;
        }

        private static ProcessInfo GetProcessInfo(ManagementBaseObject newEvent)
        {
            return ProcessInfo.GetProcessById((uint)newEvent["ProcessID"]);
        }

        private static void StopWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            ProcessInfo processInfo = GetProcessInfo(e.NewEvent);
            if (processInfo == null)
                return;
            ProcessInfo parentInfo = ProcessInfo.GetProcessById(processInfo.ParentPID);
            using (var context = new SysGuardDbContext())
            {
                var process = context.ProcInfos.AddOrFind(ProcInfo.Create(processInfo));
                var parent = context.ProcInfos.AddOrFind(ProcInfo.Create(parentInfo));
                var log = new Log
                {
                    PID = (int)processInfo.PID,
                    Process = process,
                    ParentPID = (int)processInfo.ParentPID,
                    Parent = parent,
                    DateTime = processInfo.CreationDate,
                    Action = ActionType.Start,
                };
                context.Logs.Add(log);
                context.SaveChanges();
            }
        }

        private static void StartWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            ProcessInfo processInfo = GetProcessInfo(e.NewEvent);
            if (processInfo == null)
                return;
            ProcessInfo parentInfo = ProcessInfo.GetProcessById(processInfo.ParentPID);
            using (var context = new SysGuardDbContext())
            {
                var process = context.ProcInfos.AddOrFind(ProcInfo.Create(processInfo));
                var parent = context.ProcInfos.AddOrFind(ProcInfo.Create(parentInfo));
                var log = new Log
                {
                    PID = (int)processInfo.PID,
                    Process = process,
                    ParentPID = (int)processInfo.ParentPID,
                    Parent = parent,
                    DateTime = processInfo.CreationDate,
                    Action = ActionType.Start,
                };
                context.Logs.Add(log);
                context.SaveChanges();
            }
        }
    }
}
