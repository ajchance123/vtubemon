using Autofac;
using VTubeMon.API;
using VTubeMon.Discord;

namespace VTubeMon.Wpf.Core.IOC
{
    public class DiscordModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VTubeMonDiscord>().As<IVTubeMonServerConnection>().SingleInstance();
        }
    }
}
