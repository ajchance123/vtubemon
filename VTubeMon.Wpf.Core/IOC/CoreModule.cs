using Autofac;
using System;
using VTubeMon.API;
using VTubeMon.API.Core;
using VTubeMon.API.Data.Objects;
using VTubeMon.Core;
using VTubeMon.Data.Commands;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core.IOC
{
    public class CoreModule : Module
    {
        public CoreModule()
        {
            _vTubeMonCoreGameFactories = new VTubeMonCoreGameFactories(RegisterFactory, DailyCheckinFactory, DailyQueryCommandFactory);
        }

        private IDbNonQueryCommand RegisterFactory(ulong user, ulong guild, int vtuberCash)
        {
            return new InsertCommand<User>("users", new User(user, guild, vtuberCash));
        }

        private IDbQueryCommand<IDaily> DailyQueryCommandFactory(ulong user, ulong guild)
        {
            return null; // TODO  code a new command and then create and return it
        }

        private IDbNonQueryCommand DailyCheckinFactory(ulong user, ulong guild, DateTime dateTime)
        {
            return new InsertCommand<Daily>("dailies", new Daily(user, guild, dateTime));
        }

        private VTubeMonCoreGameFactories _vTubeMonCoreGameFactories;
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VTubeMonCoreGame>().As<IVTubeMonCoreGame>().SingleInstance();
            builder.RegisterInstance(_vTubeMonCoreGameFactories);
            builder.RegisterBuildCallback(OnBuild);
        }

        private void OnBuild(ILifetimeScope obj)
        {
            
        }
    }
}
