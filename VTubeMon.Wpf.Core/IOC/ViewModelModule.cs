using Autofac;
using VTubeMon.Wpf.Core.Components.Database;
using VTubeMon.Wpf.Core.Components.Database.Users.Details;
using VTubeMon.Wpf.Core.Components.Database.Users.Values;
using VTubeMon.Wpf.Core.Components.Database.Items.Values;
using VTubeMon.Wpf.Core.Components.Discord;
using VTubeMon.Wpf.Core.Components.Settings;

namespace VTubeMon.Wpf.Core.IOC
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowViewModel>().SingleInstance();
            builder.RegisterType<DatabaseViewModel>().SingleInstance();
            builder.RegisterType<DiscordViewModel>().SingleInstance();
            builder.RegisterType<SettingsViewModel>().SingleInstance();
            builder.RegisterType<UserViewModel>().SingleInstance();
            builder.RegisterType<UserSettingsDetailCollectionViewModel>().SingleInstance();
            builder.RegisterType<ItemViewModel>().SingleInstance();
        }
    }
}
