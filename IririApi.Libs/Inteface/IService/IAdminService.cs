using IririApi.Libs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model.IService
{
    public interface IAdminService
    {
        List<MemberRegistrationUser> GetAllMembersAsync();
        Task AddNewMemberAsync(MemberUserViewModel model);

        HttpResponseMessage DeleteMemberAsync(string id);
    }

}
