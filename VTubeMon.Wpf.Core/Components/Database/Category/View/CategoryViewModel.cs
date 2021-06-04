using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using VTubeMon.API;
using VTubeMon.Data.Commands.QueryCommands;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.Components.Database.Category.View
{
    public class CategoryViewModel : BindableBase
    {
        public CategoryViewModel(StringsService stringService, IVTubeMonDbConnection vTubeMonDbConnection)
        {
            _stringService = stringService;
            _vTubeMonDbConnection = vTubeMonDbConnection;
            ItemCategoryCollection = new ObservableCollection<ItemCategoryViewModel>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectItemCategoryCommand()).Select(
                u => new ItemCategoryViewModel(u, DeleteItem)));
            StatCategoryCollection = new ObservableCollection<StatCategoryViewModel>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectStatCategoryCommand()).Select(
                u => new StatCategoryViewModel(u, DeleteStat)));
        }

        private StringsService _stringService;
        private IVTubeMonDbConnection _vTubeMonDbConnection;
        public ICollection<ItemCategoryViewModel> ItemCategoryCollection { get; }
        public ICollection<StatCategoryViewModel> StatCategoryCollection { get; }

        public ICommand AddItemCatCommand => new DelegateCommand(() =>
        {
            ItemCategoryCollection.Add(new ItemCategoryViewModel(DeleteItem));
        });

        public ICommand AddStatCatCommand => new DelegateCommand(() =>
        {
            StatCategoryCollection.Add(new StatCategoryViewModel(DeleteStat));
        });

        public void DeleteItem(ItemCategoryViewModel itemView)
        {
            ItemCategoryCollection.Remove(itemView);
        }

        public void DeleteStat(StatCategoryViewModel statView)
        {
            StatCategoryCollection.Remove(statView);
        }

        public void SaveItem(ItemCategoryViewModel itemView)
        {

        }

        public void SaveStat(StatCategoryViewModel statView)
        {

        }
    }
}
