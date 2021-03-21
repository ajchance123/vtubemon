using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using VTubeMon.Wpf.Core.Themes;

namespace VTubeMon.Wpf.Core.IOC
{
    public class SettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ThemeService>().SingleInstance();
        }
    }
}
