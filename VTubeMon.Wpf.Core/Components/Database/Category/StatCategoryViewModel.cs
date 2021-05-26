using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core.Components.Database.Category
{
    public class StatCategoryViewModel : BindableBase
    {
        public StatCategoryViewModel(IStatCategory statCategory)
        {
            StatCategory = statCategory;
            Name = statCategory.StatName.Value;
        }

        public StatCategoryViewModel()
        {
            StatCategory = new StatCategory();
        }

        public IStatCategory StatCategory { get; }
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
