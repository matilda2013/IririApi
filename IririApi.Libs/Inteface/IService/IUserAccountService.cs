using IririApi.Libs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model.IService
{
    public interface IUserAccountService
    {
        Task RegisterAdminUserAsync(MemberUserViewModel model);
        Task RegisterMemberUserAsync(MemberUserViewModel model);
        Task<string> ViewMembersByIdAsync(string userEmail)
    }
}
