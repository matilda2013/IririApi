using Autofac;
using IririApi.Libs.Model.IService;
using IririApi.Libs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Common.iocContainer
{
    public class ExternalServicesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.Name.EndsWith("Service")))
                .As(t => t.GetInterfaces().Where(i => i.Name.EndsWith("Service")))
                .InstancePerLifetimeScope();


        }
    }
}
