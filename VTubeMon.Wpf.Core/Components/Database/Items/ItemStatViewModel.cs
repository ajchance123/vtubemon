using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Commands.QueryCommands;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core.Components.Database.Items
{
    public class ItemStatViewModel : BindableBase
    {
        public ItemStatViewModel(IVTubeMonDbConnection vTubeMonDbConnection, IItemStat itemStat = null)
        {
            _vTubeMonDbConnection = vTubeMonDbConnection;
            ItemStat = itemStat;
            StatValue = itemStat.StatValue.Value;
            if (itemStat != null)
                SetName();
        }

        public ItemStatViewModel()
        {
            ItemStat = new ItemStat();
        }

        private IVTubeMonDbConnection _vTubeMonDbConnection;
        public IItemStat ItemStat { get; set; }
        public bool HasValue => ItemStat != null;
        private int _statValue;
        private string _statName;

        public int StatValue
        {
            get => _statValue;
            set => SetProperty(ref _statValue, value);
        }

        public string StatName
        {
            get => _statName;
            set => SetProperty(ref _statName, value);
        }

        private void SetName()
        {
            ICollection<StatCategory> stats = new ObservableCollection<StatCategory>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectStatCategoryCommand(ItemStat.IdStat.Value)));
            if (stats.Any())
            {
                StatName = stats.Single().StatName.Value;
            }
        }
    }
}
