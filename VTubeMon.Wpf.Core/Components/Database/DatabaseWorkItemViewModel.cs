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
            RaisePropertyChanged(nameof(Query));
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(var newColumn in e.NewItems)
                    {
                        (newColumn as ColumnName).PropertyChanged += (s, e) => RaisePropertyChanged(nameof(Query));
                    }
                    break;
            }
        }

        private IVTubeMonDbConnection _vTubeMonDbConnection;

        public string Name { get; set; }

        private string _query;
        public string Query
        {
            get
            {
                return $"SELECT {string.Join(", ", ColumnCollection.Select(c => c.Name))} FROM {Name}";
            }
        }

        public ICommand RunCommand => new DelegateCommand(() =>
        {
            var command = new MultiStringSelectCommand(Query);
            var results = _vTubeMonDbConnection.ExecuteDbQueryCommand(command);

            ResultsCollection.Clear();
            foreach(var result in results)
            {
                ResultsCollection.Add(result);
            }

            ResultColumnNames.Clear();
            foreach (var column in command.ColumnNames) //this is cheating but meh
            {
                ResultColumnNames.Add(column);
            }
        });

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
