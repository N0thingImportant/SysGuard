﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.Model.Events
{
    class ProcessPidSelectedEvent : PubSubEvent<string>
    {
    }
}
