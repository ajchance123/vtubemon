using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using VTubeMon.API;
using VTubeMon.Data;
using VTubeMon.Data.Objects;
using VTubeMon.Wpf.Core.Components;

namespace VTubeMon.Wpf.Core
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(ILogger logger, IVTubeMonDbConnection vTubeMonDbConnection, DataCache dataCache, IVTubeMonServerConnection vTubeMonServerConnection, DatabaseWorkspace databaseWorkspace)
        {
            LogCollection = new ObservableCollection<string>();
            _logger = logger;
            _logger.OnLog += Logger_OnLog;
            _vTubeMonDbConnection = vTubeMonDbConnection;
            _vTubeMonDbConnection.OpenConnection();

            _dataCache = dataCache;
            _dataCache.RefreshAll();
            _dataCache.VtuberCache.OnDataRefreshed += VtuberCache_OnDataRefreshed;

            AgencyCollection = new ObservableCollection<Agency>(_dataCache.AgencyCache.CachedList);
            VTuberCollection = new ObservableCollection<VTuberViewModel>();
            UpdateVtuberCollection();

            _vTubeMonServerConnection = vTubeMonServerConnection;
            vTubeMonServerConnection.OnConnect += VTubeMonServerConnection_OnConnect;
            vTubeMonServerConnection.OnDisconnect += VTubeMonServerConnection_OnDisconnect;
            _vTubeMonServerConnection.CreateNewClient();

            _databaseWorkspace = databaseWorkspace;
        }

        private void VTubeMonServerConnection_OnDisconnect(object sender, bool e)
        {
            CanConnectDiscord = e;
            CanDisconnectDiscord = !e;
        }

        private void VTubeMonServerConnection_OnConnect(object sender, bool e)
        {
            CanDisconnectDiscord = e;
            CanConnectDiscord = !e;
        }

        private void Logger_OnLog(object sender, string e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                LogCollection.Add(e);
            });
        }

        public ICollection<string> LogCollection { get; }

        private bool _showLogOutputWindow;
        public bool ShowLogOutputWindow
        {
            get => _showLogOutputWindow;
            set => SetProperty(ref _showLogOutputWindow, value);
        }

        private void VtuberCache_OnDataRefreshed(object sender, IReadOnlyList<VTuber> e)
        {
            UpdateVtuberCollection();
        }

        private void UpdateVtuberCollection()
        {
            VTuberCollection.Clear();
            foreach (var vtuber in _dataCache.VtuberCache.CachedList.Select(v => new VTuberViewModel(v, AgencyCollection.Single(a => a.IdAgency.Value == v.IdAgency.Value).AgencyName.Value)))
            {
                VTuberCollection.Add(vtuber);
            }
        }

        private void Dt_Tick(object sender, System.EventArgs e)
        {
            Ping = _vTubeMonServerConnection.Ping.ToString();
        }

        private string _ping;
        public string Ping
        {
            get => _ping;
            set => SetProperty(ref _ping, value);
        }

        private ILogger _logger;
        private DataCache _dataCache;
        private IVTubeMonServerConnection _vTubeMonServerConnection;
        private IVTubeMonDbConnection _vTubeMonDbConnection;
        private ICollection<Agency> AgencyCollection { get; }
        private DatabaseWorkspace _databaseWorkspace;

        public ICollection<VTuberViewModel> VTuberCollection { get; }
        public ICommand ConnectDiscordCommand => new DelegateCommand(async () =>
        {
            CanConnectDiscord = false;
            try
            {
                await _vTubeMonServerConnection.ConnectAsync();
                CanDisconnectDiscord = true;
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                CanConnectDiscord = true;
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
            try
            {
                await _vTubeMonServerConnection.DisconnectAsync();
                CanConnectDiscord = true;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                CanDisconnectDiscord = true;
            }
        });

        private bool _canDisconnectDiscord = false;
        public bool CanDisconnectDiscord
        {
            get => _canDisconnectDiscord;
            set => SetProperty(ref _canDisconnectDiscord, value);
        }


        private bool _showDatabaseView = false;
        public bool ShowDatabaseView
        {
            get => _showDatabaseView;
            set
            {
                SetViewBoolGroup(ref _showDatabaseView);
            }
        }

        private bool _showDiscordView = true;
        public bool ShowDiscordView
        {
            get => _showDiscordView;
            set
            {
                SetViewBoolGroup(ref _showDiscordView);
            }
        }

        private bool _showGameView = false;
        public bool ShowGameView
        {
            get => _showGameView;
            set
            {
                SetViewBoolGroup(ref _showGameView);
            }
        }

        private bool _showSettingsView = false;
        public bool ShowSettingsView
        {
            get => _showSettingsView;
            set
            {
                SetViewBoolGroup(ref _showSettingsView);
            }
        }

        public void SetViewBoolGroup(ref bool trueProperty, [CallerMemberName] string propertyName = null)
        {
            _showDiscordView = false;
            RaisePropertyChanged(nameof(ShowDiscordView));

            _showDatabaseView = false;
            RaisePropertyChanged(nameof(ShowDatabaseView));

            _showGameView = false;
            RaisePropertyChanged(nameof(ShowGameView));

            _showSettingsView = false;
            RaisePropertyChanged(nameof(ShowSettingsView));

            SetProperty(ref trueProperty, true, propertyName);
        }
    }
}
