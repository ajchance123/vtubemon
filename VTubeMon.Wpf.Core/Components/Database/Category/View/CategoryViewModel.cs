using Prism.Commands;
using Prism.Mvvm;
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
            ItemCategoryCollection = new ObservableCollection<ItemCategoryViewModel>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectItemCategoryCommand()).Select(u => new ItemCategoryViewModel(u)));
            StatCategoryCollection = new ObservableCollection<StatCategoryViewModel>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectStatCategoryCommand()).Select(u => new StatCategoryViewModel(u)));
        }

        private StringsService _stringService;
        private IVTubeMonDbConnection _vTubeMonDbConnection;
        public ICollection<ItemCategoryViewModel> ItemCategoryCollection { get; }
        public ICollection<StatCategoryViewModel> StatCategoryCollection { get; }

        public ICommand AddItemCatCommand => new DelegateCommand(() =>
        {
            ItemCategoryCollection.Add(new ItemCategoryViewModel());
        });

        public ICommand AddStatCatCommand => new DelegateCommand(() =>
        {
            StatCategoryCollection.Add(new StatCategoryViewModel());
        });

        public ICommand DeleteItemCatCommand => new DelegateCommand<ItemCategoryViewModel>((item) =>
        {
            ItemCategoryCollection.Remove(item);
        });

        public ICommand DeleteStatCatCommand => new DelegateCommand<StatCategoryViewModel>((stat) =>
        {
            StatCategoryCollection.Remove(stat);
        });
    }
}
