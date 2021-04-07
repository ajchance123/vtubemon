using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace VTubeMon.Wpf.Core.Components.Database
{
    public interface IDatabaseWorkItemActionParameter
    {
        string Name { get; }
        string ParameterValue { get; set; }

        ICommand ParameterCommand { get; }
        string CommandName { get; }
        bool ShowCommand { get; }

        bool ShowList { get; }
        ICollection<string> ParameterValueCollection { get; }
    }
}
