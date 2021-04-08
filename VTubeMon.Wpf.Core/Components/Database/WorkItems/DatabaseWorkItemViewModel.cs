using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using VTubeMon.API;
using VTubeMon.Data.Commands;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.Components.Database.WorkItems
{
    public abstract class DatabaseWorkItemViewModelBase : BindableBase
    {
        public DatabaseWorkItemViewModelBase(StringsService stringsService, IVTubeMonDbConnection vTubeMonDbConnection, ICollection<DatabaseWorkItemActionViewModelBase> nonQueryActions)
        {
            var columnCollection = new ObservableCollection<ColumnName>();
            columnCollection.CollectionChanged += ColumnCollection_CollectionChanged;
            ColumnCollection = columnCollection;

            columnCollection.Add(new ColumnName() { Name = "*" });

            _vTubeMonDbConnection = vTubeMonDbConnection;
            ResultsCollection = new ObservableCollection<ICollection<string>>();
            ResultColumnNames = new ObservableCollection<string>();
            Name = stringsService.Translate(Table).ToString();

            NonQueryActions = nonQueryActions;

            foreach (var nonQueryAction in NonQueryActions)
            {
                nonQueryAction.OnCommandCreated += (s, e) => NonQuery = e.Statement;
            }
        }

        public virtual ICollection<DatabaseWorkItemActionViewModelBase> NonQueryActions { get; }

        public abstract string Table { get; }
        public string Name { get; }

        private void ColumnCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetAutoQuery();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var newColumn in e.NewItems)
                    {
                        (newColumn as ColumnName).PropertyChanged += (s, e) => SetAutoQuery();
                    }
                    break;
            }
        }

        private void SetAutoQuery()
        {
            Query = $"SELECT {string.Join(", ", ColumnCollection.Select(c => c.Name))} FROM {Table}";
        }

        protected IVTubeMonDbConnection _vTubeMonDbConnection;


        private string _query;
        public string Query
        {
            get => _query;
            set => SetProperty(ref _query, value);
        }

        public ICommand RunQueryCommand => new DelegateCommand(() =>
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
                IsQueryResultVisible = true;
                IsNonQueryResultVisible = false;
            }
            catch(Exception ex)
            {
                SqlError = ex.Message;
            }
        });


        private bool _isQueryResultVisible;
        public bool IsQueryResultVisible
        {
            get => _isQueryResultVisible;
            set => SetProperty(ref _isQueryResultVisible, value);
        }

        private bool _isNonQueryResultVisible;
        public bool IsNonQueryResultVisible
        {
            get => _isNonQueryResultVisible;
            set => SetProperty(ref _isNonQueryResultVisible, value);
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
                IsQueryResultVisible = false;
                IsNonQueryResultVisible = false;
                IsErrorVisible = true;
                SetProperty(ref _sqlError, value);
            }
        }
        public ICollection<ColumnName> ColumnCollection { get; }

        public ICollection<ICollection<string>> ResultsCollection { get; }
        public ICollection<string> ResultColumnNames { get; }

        public ICommand RunNonQueryCommand => new DelegateCommand(() =>
        {
            try
            {

                var command = new NonQueryCommand(this.Table, NonQuery);
                var result = _vTubeMonDbConnection.ExecuteDbNonQueryCommand(command);

                IsErrorVisible = false;
                IsQueryResultVisible = false;
                IsNonQueryResultVisible = true;

                NonQueryResult = $"({result}) Row(s) affected";
            }
            catch (Exception ex)
            {
                SqlError = ex.Message;
            }
        });


        private string _nonQuery;
        public string NonQuery
        {
            get => _nonQuery;
            set => SetProperty(ref _nonQuery, value);
        }

        private string _nonQueryResult;
        public string NonQueryResult
        {
            get => _nonQueryResult;
            set => SetProperty(ref _nonQueryResult, value);
        }
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
