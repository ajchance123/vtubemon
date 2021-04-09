using Prism.Mvvm;
using VTubeMon.API;

namespace VTubeMon.Wpf.Core.Components.Discord
{
    public class ChannelViewModel : BindableBase
    {
        public ChannelViewModel(IChannel channel)
        {
            _channel = channel;
        }

        private IChannel _channel;

        public string Name => _channel.Name;

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public void SendMessage(string message)
        {
            _channel.SendMessage(message);
        }
        public void SendMessage(string message, string fileName)
        {
            _channel.SendMessage(message, fileName);
        }
    }
}
