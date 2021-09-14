using AutoMapper;
using IririApi.Libs.Model;
using IririApi.Libs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Helpers
{
    public class DomainServicesMapperProfile : Profile
    {
        public DomainServicesMapperProfile()
        {
            CreateMap<MemberRegistrationUser, MemberUserViewModel>();

        }
    }
}
