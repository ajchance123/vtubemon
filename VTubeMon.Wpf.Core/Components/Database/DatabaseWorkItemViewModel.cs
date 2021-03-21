using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Input;
using VTubeMon.API;
using VTubeMon.Data.Commands;

namespace VTubeMon.Wpf.Core.Components.Database
{
    public class DatabaseWorkItemViewModel : BindableBase
    {
        public DatabaseWorkItemViewModel(IVTubeMonDbConnection vTubeMonDbConnection)
        {
            var columnCollection = new ObservableCollection<ColumnName>();
            columnCollection.CollectionChanged += ColumnCollection_CollectionChanged;
            ColumnCollection = columnCollection;

            columnCollection.Add(new ColumnName() { Name = "*" });

            _vTubeMonDbConnection = vTubeMonDbConnection;
            ResultsCollection = new ObservableCollection<ICollection<string>>();
            ResultColumnNames = new ObservableCollection<string>();
        }

        private void ColumnCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetAutoQuery();
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(var newColumn in e.NewItems)
                    {
                        (newColumn as ColumnName).PropertyChanged += (s, e) => SetAutoQuery();
                    }
                    break;
            }
        }

        private void SetAutoQuery()
        {
            Query = $"SELECT {string.Join(", ", ColumnCollection.Select(c => c.Name))} FROM {Name}";
        }

        private IVTubeMonDbConnection _vTubeMonDbConnection;

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                SetAutoQuery();
            }
        }

        private string _query;
        public string Query
        {
            get => _query;
            set => SetProperty(ref _query, value);
        }

        public ICommand RunCommand => new DelegateCommand(() =>
        {
            try
            {

                var command = new MultiStringSelectCommand(Query);
                var results = _vTubeMonDbConnection.ExecuteDbQueryCommand(command);

                ResultsCollection.Clear();
                foreach (var result in results)
                {
                    ResultsCollection.Add(result);
                }

                ResultColumnNames.Clear();
                foreach (var column in command.ColumnNames) //this is cheating but meh
                {
                    ResultColumnNames.Add(column);
                }
                IsErrorVisible = false;
                IsResultVisible = true;
            }
            catch(Exception ex)
            {
                SqlError = ex.Message;
            }
        });

        private bool _isResultVisible;
        public bool IsResultVisible
        {
            get => _isResultVisible;
            set => SetProperty(ref _isResultVisible, value);
        }


        private bool _isErrorVisible;
        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set => SetProperty(ref _isErrorVisible, value);
        }

        private string _sqlError;
        public string SqlError
        {
            get => _sqlError;
            set
            {
                IsResultVisible = false;
                IsErrorVisible = true;
                SetProperty(ref _sqlError, value);
            }
        }
        public ICollection<ColumnName> ColumnCollection { get; }

        public ICollection<ICollection<string>> ResultsCollection { get; }
        public ICollection<string> ResultColumnNames { get; }
    }

    public class ColumnName : BindableBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }

}
