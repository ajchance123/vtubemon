using System;
using VTubeMon.API;

namespace VTubeMon.Common
{
    public class EventLogger : ILogger
    {
        public event EventHandler<string> OnLog;

        public void Log(string log)
        {
            OnLog?.Invoke(this, log);
        }

        public void Log(Exception e)
        {
            OnLog?.Invoke(this, e.ToString());
        }
    }
}
