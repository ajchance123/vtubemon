using Autofac;
using VTubeMon.Discord;

namespace VTubeMon.Wpf.Core.IOC
{
    public class DiscordModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VTubeMonDiscord>().SingleInstance();
        }
    }
}
