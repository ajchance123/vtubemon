using Autofac;
using VTubeMon.API.Core;
using VTubeMon.Core;

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
