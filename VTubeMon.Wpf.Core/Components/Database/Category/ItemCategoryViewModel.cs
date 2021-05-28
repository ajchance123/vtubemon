using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core.Components.Database.Category
{
   public class ItemCategoryViewModel : BindableBase
    {
        public ItemCategoryViewModel(IItemCategory itemCategory, Action<ItemCategoryViewModel> onDelete)
        {
            ItemCategory = itemCategory;
            Name = itemCategory.CategoryName.Value;
            OnDelete = onDelete;
        }

        public ItemCategoryViewModel(Action<ItemCategoryViewModel> onDelete)
        {
            ItemCategory = new ItemCategory();
            OnDelete = onDelete;
        }

        Action<ItemCategoryViewModel> OnDelete;
        public IItemCategory ItemCategory { get; }
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ICommand DeleteItemCommand => new DelegateCommand(() =>
        {
            OnDelete(this);
        });
    }
}
