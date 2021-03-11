using Autofac;
using VTubeMon.Wpf.Core.Components.Database;
using VTubeMon.Wpf.Core.Components.Discord;

namespace VTubeMon.Wpf.Core.IOC
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowViewModel>().SingleInstance();
            builder.RegisterType<DatabaseViewModel>().SingleInstance();
            builder.RegisterType<DiscordViewModel>().SingleInstance();
        }
    }
}
