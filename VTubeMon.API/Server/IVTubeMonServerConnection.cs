using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VTubeMon.API.Server;

namespace VTubeMon.API
{
    public interface IVTubeMonServerConnection
    {
        int Ping { get; }

        event EventHandler<bool> OnConnect;
        event EventHandler<bool> OnDisconnect;
        event EventHandler<bool> OnReadyChanged;

        Task ConnectAsync();
        void CreateNewClient(string prefix = "v!");
        Task DisconnectAsync();
        IEnumerable<IServer> Servers { get; }
    }
}