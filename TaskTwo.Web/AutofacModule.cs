using Autofac;
using TaskTwo.Data.Interfaces;
using TaskTwo.Data.Repositories;
using TaskTwo.Logic.Interfaces;
using TaskTwo.Logic.MessageHandlers;
using System.Linq;

namespace TaskTwo.Web
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IBaseService).Assembly)
            .Where(t => typeof(IBaseService).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(IBaseDecorator).Assembly)
            .Where(t => typeof(IBaseDecorator).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(IMessageHandler).Assembly)
            .Where(t => typeof(IMessageHandler).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(IRepository<>).Assembly)
                .AsClosedTypesOf(typeof(IRepository<>))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>();
            builder.RegisterType<MessageFactory>()
                .As<IMessageFactory>();
        }
    }
}