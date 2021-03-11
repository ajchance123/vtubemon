using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTubeMon.API;

namespace VTubeMon.Wpf.Core.Components.Database
{
    public class DatabaseViewModel : BindableBase
    {
        public DatabaseViewModel(IVTubeMonDbConnection vTubeMonDbConnection, DatabaseWorkspace databaseWorkspace)
        {
            _databaseWorkspace = databaseWorkspace;
            _vTubeMonDbConnection = vTubeMonDbConnection;
            QueryCollection = new ObservableCollection<DatabaseWorkItemViewModel>()
            {
                new DatabaseWorkItemViewModel(vTubeMonDbConnection)
                {
                    Name = "vtubers"
                },
                new DatabaseWorkItemViewModel(vTubeMonDbConnection)
                {
                    Name = "dailies"
                },
                new DatabaseWorkItemViewModel(vTubeMonDbConnection)
                {
                    Name = "users"
                },
                new DatabaseWorkItemViewModel(vTubeMonDbConnection)
                {
                    Name = "agencies"
                },
            };
        }

        public string Name { get; set; }
        private DatabaseWorkspace _databaseWorkspace;
        private IVTubeMonDbConnection _vTubeMonDbConnection;

        public ICollection<DatabaseWorkItemViewModel> QueryCollection { get; }
    }
}
