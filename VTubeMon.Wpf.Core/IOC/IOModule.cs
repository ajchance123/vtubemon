using Autofac;
using VTubeMon.API;
using VTubeMon.Wpf.Core.IO;

namespace VTubeMon.Wpf.Core.IOC
{
    public class IOModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileService>().As<IFileService>().SingleInstance();
            builder.RegisterType<IOService>().As<IIOService>().SingleInstance();
        }
    }
}
