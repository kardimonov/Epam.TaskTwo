using Autofac;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Logic.Interfaces;
using System.Linq;

namespace ITAcademy.TaskTwo.Web
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
        }
    }
}