using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core.Components.Database.Items
{
    public class StatCategoryViewModel : BindableBase
    {
        public StatCategoryViewModel(IStatCategory statCategory)
        {
            StatCategory = statCategory;
            Value = StatCategory.StatName.ToString();
        }

        public StatCategoryViewModel()
        {
            StatCategory = new StatCategory();
        }

        public IStatCategory StatCategory { get; }
        private string _value;
        
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public ICommand DeleteCommand => new DelegateCommand(() =>
        {

        });
    }
}
