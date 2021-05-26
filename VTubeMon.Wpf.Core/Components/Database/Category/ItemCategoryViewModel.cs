using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core.Components.Database.Category
{
   public class ItemCategoryViewModel : BindableBase
    {
        public ItemCategoryViewModel(IItemCategory itemCategory)
        {
            ItemCategory = itemCategory;
            Name = itemCategory.CategoryName.Value;
        }

        public ItemCategoryViewModel()
        {
            ItemCategory = new ItemCategory();
        }

        public IItemCategory ItemCategory { get; }
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
