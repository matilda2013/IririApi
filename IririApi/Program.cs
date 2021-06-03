using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using IririApi.Libs.Common.iocContainer;
using IririApi.Libs.Common.NewFolder;
using IririApi.Libs.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IririApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            var config = new MapperConfiguration(cfg => {

                cfg.AddProfile<DomainServicesMapperProfile>();
            });
 
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });




    }
}
