using System;
using System.Threading.Tasks;

namespace VTubeMon.API
{
    public interface IVTubeMonServerConnection
    {
        int Ping { get; }

        event EventHandler<bool> OnConnect;
        event EventHandler<bool> OnDisconnect;

        Task ConnectAsync();
        void CreateNewClient(string prefix = "v!");
        Task DisconnectAsync();
    }
}