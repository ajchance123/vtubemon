using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using VTubeMon.Core;
using VTubeMon.Data;
using VTubeMon.Data.Objects;
using VTubeMon.MySql;

namespace VTubeMon.Wpf.Core
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            _vTubeMonDbConnection = new VTubeMonDbConnection();
            _vTubeMonDbConnection.OpenConnection();

            _dataCache = new DataCache(_vTubeMonDbConnection);
            _dataCache.VtuberCache.OnDataRefreshed += VtuberCache_OnDataRefreshed;

            AgencyCollection = new ObservableCollection<Agency>(_dataCache.AgencyCache.CachedList);
            VTuberCollection = new ObservableCollection<VTuberViewModel>();
            UpdateVtuberCollection();

            _vTubeMonDiscord = new VTubeMonDiscord(_dataCache, _vTubeMonDbConnection);
            _vTubeMonDiscord.CreateNewClient();
            ConnectClient();

            DispatcherTimer dt = new DispatcherTimer();
            dt.Tick += Dt_Tick;
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Start();
        }

        private async void ConnectClient()
        {
            await _vTubeMonDiscord.Client.ConnectAsync();
        }

        private void VtuberCache_OnDataRefreshed(object sender, IReadOnlyList<VTuber> e)
        {
            UpdateVtuberCollection();
        }

        private void UpdateVtuberCollection()
        {
            VTuberCollection.Clear();
            foreach (var vtuber in _dataCache.VtuberCache.CachedList.Select(v => new VTuberViewModel(v, AgencyCollection.Single(a => a.IdAgency.Value == v.Affiliation.Value).Name.Value)))
            {
                VTuberCollection.Add(vtuber);
            }
        }

        private void Dt_Tick(object sender, System.EventArgs e)
        {
            Ping = _vTubeMonDiscord.Client.Ping.ToString();
        }

        private string _ping;
        public string Ping
        {
            get => _ping;
            set => SetProperty(ref _ping, value);
        }

        private DataCache _dataCache;
        private VTubeMonDiscord _vTubeMonDiscord;
        private VTubeMonDbConnection _vTubeMonDbConnection;

        private ICollection<Agency> AgencyCollection { get; }
        public ICollection<VTuberViewModel> VTuberCollection { get; }

    }
}
