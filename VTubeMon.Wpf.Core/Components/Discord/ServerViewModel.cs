using Prism.Mvvm;
using VTubeMon.API.Server;

namespace VTubeMon.Wpf.Core.Components.Discord
{
    public class ServerViewModel : BindableBase
    {
        public ServerViewModel(IServer server)
        {
            _server = server;
        }

        private IServer _server;

        public string ServerName => _server.Name;
        private bool _isServerSelected;
        public bool IsServerSelected
        {
            get => _isServerSelected;
            set => SetProperty(ref _isServerSelected, value);
        }

        public void SendMessage(string message)
        {
            _server.SendMessage(message);
        }
    }
}
