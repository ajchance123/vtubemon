using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core.Components.Database.Category
{
    public class StatCategoryViewModel : BindableBase
    {
        public StatCategoryViewModel(IStatCategory statCategory, Action<StatCategoryViewModel> onDelete)
        {
            StatCategory = statCategory;
            Name = statCategory.StatName.Value;
            OnDelete = onDelete;
        }

        public StatCategoryViewModel(Action<StatCategoryViewModel>  onDelete)
        {
            StatCategory = new StatCategory();
            OnDelete = onDelete;
        }

        Action<StatCategoryViewModel> OnDelete;
        public IStatCategory StatCategory { get; }
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ICommand DeleteStatCommand => new DelegateCommand(() =>
        {
            OnDelete(this);
        });
    }
}
