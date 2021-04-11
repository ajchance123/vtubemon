using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API
{
    public interface IChannel
    {
        string Name { get; }
        void SendMessage(string message);
        void SendMessage(string message, string fileName);
    }
}
