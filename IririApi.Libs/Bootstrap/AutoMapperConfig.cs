
using AutoMapper;
using IririApi.Libs.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Bootstrap
{
    public static class AutoMapperConfig
    {
        public static void InitializeMapperProfiles()
        {

            var config = new MapperConfiguration(cfg => {
             
                cfg.AddProfile<DomainServicesMapperProfile>();
            });
        }
    }
}
