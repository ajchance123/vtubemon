﻿using Autofac;
using VTubeMon.API;
using VTubeMon.Data;
using VTubeMon.MySql;

namespace VTubeMon.Wpf.Core.IOC
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VTubeMonMySqlConnection>().As<IVTubeMonDbConnection>().SingleInstance();
            builder.RegisterType<DataCache>().SingleInstance();
        }
    }
}
