﻿using System.Collections.Generic;
using System.Threading;

namespace ProcessMonitor.Model.Logic
{
    public static class AppThreadManager
    {
        private static readonly List<Thread> threads = new List<Thread>();

        public static void AddThread(Thread t)
        {
            threads.Add(t);
        }

        public static void TerminateEvery()
        {
            foreach (var thread in threads)
            {
                if (thread.IsAlive)
                    thread.Abort();
            }
        }
    }
}
