using Prism.Events;
using System.Threading;

namespace ProcessMonitor.Model.Events
{
    class ThreadMadeEvent : PubSubEvent<Thread>
    {
    }
}
