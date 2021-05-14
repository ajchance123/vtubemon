using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Commands;
using VTubeMon.Data.Commands.QueryCommands;
using VTubeMon.Data.Commands.QueryCommands.ItemCommands;
using VTubeMon.Data.Objects;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.Components.Database.Items.Values
{
    public class ItemViewModel : BindableBase
    {
        public ItemViewModel(StringsService stringService, IVTubeMonDbConnection vTubeMonDbConnection)
        {
            _stringService = stringService;
            _vTubeMonDbConnection = vTubeMonDbConnection;
            ItemCollection = new ObservableCollection<IItem>(vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectItemCommand()));
            ItemStatCollection = new ObservableCollection<ItemStatViewModel>();
            ItemCategoryCollection = new ObservableCollection<ItemCategory>();

            InitializeItemStats();
            InitializeItemCategory();
        }

        private StringsService _stringService;
        private IVTubeMonDbConnection _vTubeMonDbConnection;
        private IItem _selectedItem;
        private IItemCategory _selectedCategory;
        private IList<ItemStat> _selectedItemStatValues;
        public ICollection<IItem> ItemCollection { get; }
        public ICollection<ItemCategory> ItemCategoryCollection { get; }
        public ICollection<ItemStatViewModel> ItemStatCollection { get; }

        public IItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateItemStats();
                UpdateItemCategory(_selectedItem.IdCategory.Value);
            }
        }

        public IItemCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
            }
        }

        private void InitializeItemStats()
        {
            IList<ItemStat> _itemStats = new List<ItemStat>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectItemStatCommand()));
            foreach(var stat in _itemStats)
            {
                ItemStatCollection.Add(new ItemStatViewModel(_vTubeMonDbConnection, stat));
            }
        }

        private void InitializeItemCategory()
        {
            IList<ItemCategory> _itemCategories = new List<ItemCategory>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectItemCategoryCommand()));
            foreach(var itemCat in _itemCategories)
            {
                ItemCategoryCollection.Add(itemCat);
            }
        }

        private void UpdateItemStats()
        {
            _selectedItemStatValues = new List<ItemStat>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectItemStatCommand(SelectedItem)));

            foreach (var stat in ItemStatCollection)
            {
                stat.ItemStat = _selectedItemStatValues.SingleOrDefault(u => u.IdStat.Value == stat.ItemStat.IdStat.Value);
            }
        }

        private void UpdateItemCategory(int idCat)
        {
            foreach(var cat in ItemCategoryCollection)
            {
                if(cat.IdCategory.Value == idCat)
                {
                    SelectedCategory = cat;
                    break;
                }
            }
        }

        public ICommand SaveCommand => new DelegateCommand(() =>
        {
            var currentCollection = ItemStatCollection.ToList();
        });

        public ICommand AddCommand => new DelegateCommand(() =>
        {
            ItemStatCollection.Add(new ItemStatViewModel());
        });
    }
}
