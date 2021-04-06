using Autofac;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.IOC
{
    public class SettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ThemeService>().SingleInstance();
            builder.RegisterType<StringsService>().SingleInstance();
        }
    }
}
