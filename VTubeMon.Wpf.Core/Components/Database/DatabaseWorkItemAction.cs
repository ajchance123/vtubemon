using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VTubeMon.API;

namespace VTubeMon.Wpf.Core.Components.Database
{
    public class DatabaseWorkItemAction
    {
        public DatabaseWorkItemAction(string name, Func<IDbNonQueryCommand> dbNonQueryFactory, params DatabaseWorkItemActionParameter[] actionParameters)
        {
            Name = name;
            _dbNonQueryFactory = dbNonQueryFactory;
            ActionParameters = new ObservableCollection<DatabaseWorkItemActionParameter>();

        }
        public string Name { get; }

        public ICollection<DatabaseWorkItemActionParameter> ActionParameters { get; }
        private Func<IDbNonQueryCommand> _dbNonQueryFactory;

        public IVTubeMonDbConnection VTubeMonDbConnection { get; set; }

        public ICommand ExecuteActionCommand => new DelegateCommand(ExecuteAction);

        public void ExecuteAction()
        {
            var command = _dbNonQueryFactory();
            if (command == null) return;

            VTubeMonDbConnection.ExecuteDbNonQueryCommand(command);
        }

    }
}
