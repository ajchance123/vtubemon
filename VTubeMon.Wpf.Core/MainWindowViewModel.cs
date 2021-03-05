using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTubeMon.Data;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            _vTubeMonDbConnection = new VTubeMonDbConnection();
            _vTubeMonDbConnection.OpenConnection();


            VTuberCollection = new ObservableCollection<VTuber>(_vTubeMonDbConnection.ReadVTubers());
        }

        private VTubeMonDbConnection _vTubeMonDbConnection;
        public ICollection<VTuber> VTuberCollection { get; }

    }
}
