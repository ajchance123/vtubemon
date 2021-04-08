using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Wpf.Core.Components.Database.WorkItems.ActionParameters
{
    public class VTuberSelectionActionParameter : BindableBase, IDatabaseWorkItemActionParameter
    {
        public VTuberSelectionActionParameter(IEnumerable<IVTuber> vtubers)
        {
            foreach(var vtuber in vtubers)
            {
                _vtuberToIdMap.Add(vtuber.EnName.Value, vtuber.IdVtubers.Value);
            }
        }
        public string Name => "Select VTuber";

        private Dictionary<string, int> _vtuberToIdMap = new Dictionary<string, int>();

        private string _parameterValue;
        public string ParameterValue
        {
            get => _parameterValue;
            set => SetProperty(ref _parameterValue, value);
        }

        public int VtuberId => _vtuberToIdMap[ParameterValue];

        public ICommand ParameterCommand { get; }
        public string CommandName { get; }
        public bool ShowCommand => false;


        public bool ShowList => true;
        public ICollection<string> ParameterValueCollection => _vtuberToIdMap.Keys;
    }
}
