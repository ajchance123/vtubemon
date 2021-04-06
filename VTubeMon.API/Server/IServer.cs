using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API.Server
{
    public interface IServer
    {
        ulong Id { get; }
        string Name { get; }
        void SendMessageToDefaultChannel(string message);
        void SendMessageToDefaultChannel(string message, string fileName);
        IEnumerable<IChannel> Channels { get; }
    }
}
