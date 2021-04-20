using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core.Components.Database.Users
{
    public class UserSettingsDetailViewModel : BindableBase
    {
        public UserSettingsDetailViewModel(IUserSettingsDetail userSettingDetail)
        {
            UserSettingDetail = userSettingDetail;
            Detail = userSettingDetail.Detail.Value;
        }

        public UserSettingsDetailViewModel()
        {
            UserSettingDetail = new UserSettingsDetail();
        }

        public IUserSettingsDetail UserSettingDetail { get; }

        private string _detail;
        public string Detail
        {
            get => _detail;
            set => SetProperty(ref _detail, value);
        }
        public ICommand DeleteCommand => new DelegateCommand(() =>
        {

        });

    }
}
