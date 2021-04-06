using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;using System.Windows.Input;
using System.Windows.Threading;
using VTubeMon.API;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace VTubeMon.Wpf.Core.Components.Discord
{
    public class DiscordViewModel : BindableBase
    {
        public DiscordViewModel(IVTubeMonServerConnection vtubeMonServerConnection, ILogger logger)
        {
            ServerCollection = new ObservableCollection<ServerViewModel>();

            _logger = logger;
            _vtubeMonServerConnection = vtubeMonServerConnection;

            vtubeMonServerConnection.OnConnect += VtubeMonServerConnection_OnConnect;
            _vtubeMonServerConnection.OnDisconnect += _vtubeMonServerConnection_OnDisconnect;
            _vtubeMonServerConnection.OnReadyChanged += _vtubeMonServerConnection_OnReadyChanged;
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += UiTimerUpdate;
            UiInterval = 1000;
        }

        private void _vtubeMonServerConnection_OnReadyChanged(object sender, bool e)
        {
            RefreshServers();
        }

        private void VtubeMonServerConnection_OnConnect(object sender, bool e)
        {
            if(e)
            {
                Status = "Connected";
                RefreshServers();
            }
            else
            {
                Status = "Connecting faled";
            }
        }

        private void RefreshServers()
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                ServerCollection.Clear();
                foreach (var server in _vtubeMonServerConnection.Servers)
                {
                    ServerCollection.Add(new ServerViewModel(server));
                }
            });
        }

        private void _vtubeMonServerConnection_OnDisconnect(object sender, bool e)
        {
            if (e)
            {
                Status = "Disconnected";
            }
            else
            {
                Status = "Disconnecting faled";
            }
        }

        private IVTubeMonServerConnection _vtubeMonServerConnection;
        private ILogger _logger;
        private DispatcherTimer _dispatcherTimer;
        private void UiTimerUpdate(object sender, EventArgs e)
        {

        }

        private int _uiInterval;
        public int UiInterval
        {
            get => _uiInterval;
            set
            {
                _uiInterval = value;
                _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(value);
            }
        }

        public ICommand StartUiUpdatesCommand => new DelegateCommand(() =>
        {
            _dispatcherTimer.Start();
        });

        public ICommand StopUiUpdatesCommand => new DelegateCommand(() =>
        {
            _dispatcherTimer.Stop();
        });
        public ICommand RefreshServersCommmand => new DelegateCommand(() =>
        {
            RefreshServers();
        });

        public ICommand ConnectDiscordCommand => new DelegateCommand(async () =>
        {
            CanConnectDiscord = false;
            _vtubeMonServerConnection.OnConnect -= VtubeMonServerConnection_OnConnect;
            Status = "Connecting";
            try
            {
                await _vtubeMonServerConnection.ConnectAsync();
                CanDisconnectDiscord = true;
                Status = "Connected";
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                CanConnectDiscord = true;
                Status = $"Connecting failed: {ex}";
            }
            finally
            {
                _vtubeMonServerConnection.OnConnect += VtubeMonServerConnection_OnConnect;
            }
        });

        private bool _canConnectDiscord = true;
        public bool CanConnectDiscord
        {
            get => _canConnectDiscord;
            set => SetProperty(ref _canConnectDiscord, value);
        }

        public ICommand DisconnectDiscordCommand => new DelegateCommand(async () =>
        {
            CanDisconnectDiscord = false;
            Status = "Disconnecting";
            try
            {
                await _vtubeMonServerConnection.DisconnectAsync();
                CanConnectDiscord = true;
                Status = "Disconnected";
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                CanDisconnectDiscord = true;
                Status = $"Disconnecting failed: {ex}";
            }
        });

        private bool _canDisconnectDiscord = false;
        public bool CanDisconnectDiscord
        {
            get => _canDisconnectDiscord;
            set => SetProperty(ref _canDisconnectDiscord, value);
        }

        private string _status = "Disconnected";
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public ICollection<ServerViewModel> ServerCollection { get; }

        public string MessageToSend { get; set; }
        public ICommand SendMessageCommand => new DelegateCommand(() =>
        {
            foreach(var server in ServerCollection)
            {
                foreach(var channel in server.ChannelCollection.Where(c => c.IsSelected))
                {
                    if (SendFile)
                        channel.SendMessage(MessageToSend, FileAttachmentPath);
                    else
                        channel.SendMessage(MessageToSend);
                }
            }
        });

        private string _fileAttachmentPath;
        public string FileAttachmentPath
        {
            get => _fileAttachmentPath;
            set => SetProperty(ref _fileAttachmentPath, value);
        }

        public bool SendFile { get; set; }
        public ICommand BrowseFileAttachmentCommand => new DelegateCommand(() =>
        {
            OpenFileDialog ofd = new OpenFileDialog();

            var result = ofd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                FileAttachmentPath = ofd.FileName;
            }
        });
    }
}
