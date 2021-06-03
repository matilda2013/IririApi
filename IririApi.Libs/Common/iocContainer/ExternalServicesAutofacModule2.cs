using Autofac;
using IririApi.Libs.Infrastructure.Contract;
using IririApi.Libs.Model.IRepository;
using IririApi.Libs.Model.IService;
using IririApi.Libs.Repository;
using IririApi.Libs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Common.iocContainer
{
    public class ExternalServicesAutofacModule2 : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.Name.EndsWith("Repository")))
                .As(t => t.GetInterfaces().Where(i => i.Name.EndsWith("Repository")))
                .InstancePerLifetimeScope();

          

        }
    }
}

