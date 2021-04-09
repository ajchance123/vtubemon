using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using VTubeMon.API;

namespace VTubeMon.Wpf.Core.Components.Database.WorkItems.ActionParameters
{
    public class BrowseFileWorkItemActionParameter : BindableBase, IDatabaseWorkItemActionParameter
    {

        public BrowseFileWorkItemActionParameter(string fileFilter = null)
        {
            _openFileDialog = new OpenFileDialog();
            _openFileDialog.Filter = fileFilter;
            _openFileDialog.Multiselect = true;
        }
        public string Name => "Select File";

        private OpenFileDialog _openFileDialog;

        private string _parameterValue;
        public string ParameterValue
        {
            get => _parameterValue;
            set => SetProperty(ref _parameterValue, value);
        }

        public string[] FileNames { get; private set; }
        public ICommand ParameterCommand => new DelegateCommand(() =>
        {
            var result = _openFileDialog.ShowDialog();
            if(result.HasValue && result.Value)
            {
                ParameterValue = string.Join(Environment.NewLine, _openFileDialog.FileNames);
                FileNames = _openFileDialog.FileNames;
            }
        });

        public string CommandName => "Browse";
        public bool ShowCommand => true;


        public bool ShowList => false;
        public ICollection<string> ParameterValueCollection { get; }
    }
}
