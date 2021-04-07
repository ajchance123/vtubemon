using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;

namespace VTubeMon.Wpf.Core.Components.Database.WorkItems.ActionParameters
{
    public class BrowseFileWorkItemActionParameter : BindableBase, IDatabaseWorkItemActionParameter
    {

        public BrowseFileWorkItemActionParameter(string fileFilter = null)
        {
            _openFileDialog = new OpenFileDialog();
            _openFileDialog.Filter = fileFilter;
        }
        public string Name => "Select File";

        private OpenFileDialog _openFileDialog;

        private string _parameterValue;
        public string ParameterValue
        {
            get => _parameterValue;
            set => SetProperty(ref _parameterValue, value);
        }

        public ICommand ParameterCommand => new DelegateCommand(() =>
        {
            var result = _openFileDialog.ShowDialog();
            if(result.HasValue && result.Value)
            {
                ParameterValue = _openFileDialog.FileName;
            }
        });

        public string CommandName => "Browse";
        public bool ShowCommand => true;


        public bool ShowList => false;
        public ICollection<string> ParameterValueCollection { get; }
    }
}
