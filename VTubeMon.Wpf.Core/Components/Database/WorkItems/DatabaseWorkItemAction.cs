using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VTubeMon.API;

namespace VTubeMon.Wpf.Core.Components.Database
{
    public class DatabaseWorkItemAction : BindableBase
    {
        public DatabaseWorkItemAction(string name, IVTubeMonDbConnection vtubeMonDbConnection, Func<IDbNonQueryCommand> dbNonQueryFactory, params IDatabaseWorkItemActionParameter[] actionParameters)
        {
            Name = name;
            _dbNonQueryFactory = dbNonQueryFactory;
            VTubeMonDbConnection = vtubeMonDbConnection;
            ActionParameters = new ObservableCollection<IDatabaseWorkItemActionParameter>(actionParameters);

        }
        public string Name { get; }

        public ICollection<IDatabaseWorkItemActionParameter> ActionParameters { get; }
        private Func<IDbNonQueryCommand> _dbNonQueryFactory;

        public IVTubeMonDbConnection VTubeMonDbConnection { get; set; }

        public ICommand ExecuteActionCommand => new DelegateCommand(ExecuteAction);

        public void ExecuteAction()
        {
            var command = _dbNonQueryFactory();
            if (command == null) return;

            DataMessage = $"({VTubeMonDbConnection.ExecuteDbNonQueryCommand(command)}) Row(s) Affected";
        }

        private string _dataMessage;
        public string DataMessage
        {
            get => _dataMessage;
            set => SetProperty(ref _dataMessage, value);
        }

    }
}
