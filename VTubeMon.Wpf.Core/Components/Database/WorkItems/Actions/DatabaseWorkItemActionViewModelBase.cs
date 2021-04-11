using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using VTubeMon.API;

namespace VTubeMon.Wpf.Core.Components.Database.WorkItems
{
    public abstract class DatabaseWorkItemActionViewModelBase : BindableBase
    {
        public abstract string Name { get; }
        public abstract ICollection<IDatabaseWorkItemActionParameter> ActionParameters { get; }
        protected abstract IDbNonQueryCommand CreateNonDbQueryCommand();

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand CreateNonQueryCommand => new DelegateCommand(CreateNonQuery);

        private void CreateNonQuery()
        {
            try
            {
                IDbNonQueryCommand command = CreateNonDbQueryCommand();
                ErrorMessage = string.Empty;
                OnCommandCreated?.Invoke(this, command);
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public event EventHandler<IDbNonQueryCommand> OnCommandCreated;

    }
}
