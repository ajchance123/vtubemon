using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTubeMon.API.Server;

namespace VTubeMon.Wpf.Core.Components.Discord
{
    public class ServerViewModel : BindableBase
    {
        public ServerViewModel(IServer server)
        {
            _server = server;
            ChannelCollection = new ObservableCollection<ChannelViewModel>(server.Channels.Select(c => new ChannelViewModel(c)));
        }

        private IServer _server;

        public string ServerName => _server.Name;

        public void SendMessageToDefaultChannel(string message)
        {
            _server.SendMessageToDefaultChannel(message);
        }
        public void SendMessageToDefaultChannel(string message, string fileName)
        {
            _server.SendMessageToDefaultChannel(message, fileName);
        }
        public ICollection<ChannelViewModel> ChannelCollection { get; }
    }
}
