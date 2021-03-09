using Autofac;

namespace VTubeMon.Wpf.Core.IOC
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowViewModel>().SingleInstance();
        }
    }
}
