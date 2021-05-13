using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Commands.QueryCommands.ItemCommands;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core.Components.Database.Items
{
    public class ItemStatViewModel : BindableBase
    {
        public ItemStatViewModel(IVTubeMonDbConnection vtubeMonDbConnection, IItemStat itemStat = null)
        {
            ItemStat = itemStat;
            _vTubeMonDbConnection = vtubeMonDbConnection;
            StatCategories = new List<IStatCategory>();
            setStatName();
            initializeCategories();
        }

        public IItemStat ItemStat { get; set; }
        private IVTubeMonDbConnection _vTubeMonDbConnection;
        public bool HasValue => ItemStat != null;
        public ICollection<IStatCategory> StatCategories { get; }
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

        public void setStatName()
        {
            if (HasValue)
            {
                var statCategory = _vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectStatCategoryCommand(ItemStat.IdStat.Value));
                StatValue = ItemStat.StatValue.Value;
                StatName = statCategory.First().StatName.Value;
            }
        }

        public void initializeCategories()
        {
            IList<StatCategory> _statCategories = new List<StatCategory>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectStatCategoryCommand()));
            foreach (var stat in _statCategories)
            {
                StatCategories.Add(stat);
            }
        }
    }
}
