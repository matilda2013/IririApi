using IririApi.Libs.DTOs;
using IririApi.Libs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model.IService
{
    public interface IUserAccountService
    {
        Task RegisterAdminUserAsync(MemberUserViewModel model);
        Task RegisterMemberUserAsync(MemberUserViewModel model);
        MemberUserTracker ViewMembersByIdAsync(string userEmail);
        Task<HttpResponseMessage> TieMembersByCardNoAsync(string userEmail,string CardNo);
        MemberUserTracker ViewMembersByCardNoAsync(string id);
    }
}
