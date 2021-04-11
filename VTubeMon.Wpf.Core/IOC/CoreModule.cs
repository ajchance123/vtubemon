using Autofac;
using VTubeMon.API;
using VTubeMon.Game;

namespace VTubeMon.Wpf.Core.IOC
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VTubeMonCoreGame>().As<IVTubeMonCoreGame>().SingleInstance();
        }
    }
}
