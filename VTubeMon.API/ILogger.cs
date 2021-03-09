using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API
{
    public interface ILogger
    {
        event EventHandler<string> OnLog;
        void Log(string log);
        void Log(Exception ex);
    }
}
