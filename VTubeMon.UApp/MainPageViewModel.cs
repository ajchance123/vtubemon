using DSharpPlus;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace VTubeMon.UApp
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            //TODO move this logic out of the view model
            
            MessageCollection = new ObservableCollection<string>();

            _vTubeMonDiscord = new VTubeMonDiscord();
            _client = _vTubeMonDiscord.CreateNewClient();

            _client.MessageCreated += Client_MessageCreated;
            _uiTimer = new DispatcherTimer();
            _uiTimer.Interval = TimeSpan.FromSeconds(1);
            _uiTimer.Tick += UpdatePing;
        }

        private void UpdatePing(object sender, object e)
        {
            Ping = _client.Ping;
        }

        private VTubeMonDiscord _vTubeMonDiscord;
        private DiscordClient _client;
        private DispatcherTimer _uiTimer;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task Client_MessageCreated(DSharpPlus.EventArgs.MessageCreateEventArgs e)
        {
            MessageCollection.Add(e.Message.Content);
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        public ICollection<string> MessageCollection { get; }
        public ICommand ConnectCommand => new RelayCommand(Connect, true);
        public ICommand DisconnectCommand => new RelayCommand(Disconnect, true);

        private bool _canConnect = true;
        public bool CanConnect
        {
            get => _canConnect;
            set => Set(ref _canConnect, value);
        }
        private async void Connect()
        {
            await _client.ConnectAsync();
            CanConnect = false;
            CanDisconnect = true;
            _uiTimer.Start();
        }

        private bool _canDisconnect = false;
        public bool CanDisconnect
        {
            get => _canDisconnect;
            set => Set(ref _canDisconnect, value);
        }
        private async void Disconnect()
        {
            await _client.DisconnectAsync();
            CanConnect = true;
            CanDisconnect = false;
            _uiTimer.Stop();
        }

        private int _ping;
        public int Ping
        {
            get => _ping;
            set => Set(ref _ping, value);
        }
    }
}
