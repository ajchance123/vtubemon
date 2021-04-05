using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API.Server
{
    public interface IServer
    {
        ulong Id { get; }
        string Name { get; }
        void SendMessage(string message);
    }
}
