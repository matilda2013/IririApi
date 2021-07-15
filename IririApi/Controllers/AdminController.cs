using IririApi.Libs.Helpers;
using IririApi.Libs.Model;
using IririApi.Libs.Model.IService;
using IririApi.Libs.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IririApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {


        private readonly IAdminService _adminService;

        public AdminController(IAdminService userAccountService)
        {
            _adminService = userAccountService;
           
        }

        //[HttpPost]
        //[Route("AddNewClubMember")]
        //public async Task AddNewMember([FromBody] MemberUserViewModel model)
        //{

        //    ArgumentGuard.NotNullOrEmpty(model.FirstName, nameof(model.FirstName));

        //    ArgumentGuard.NotNullOrEmpty(model.LastName, nameof(model.LastName));

        //    ArgumentGuard.NotNullOrEmpty(model.MemberEmail, nameof(model.MemberEmail));

        //    await _adminService.AddNewMemberAsync(model);
        //}

        [HttpGet]
        [Route("ViewExistingClubMember")]
        public List<MemberRegistrationUser> ViewAllExisitngMembers()
        {
            return  _adminService.GetAllMembersAsync();
        }

        [HttpGet]
        [Route("GetPendingRegistrations")]
        public List<MemberRegistrationUser> GetPendingRegistrations()
        {
            return _adminService.GetPendingRegistrationsAsync();
        }

        [HttpDelete]
        [Route("DeleteMember")]
        public HttpResponseMessage DeleteEvent(string id)
        {
            return _adminService.DeleteMemberAsync(id);

        }

        [HttpPut]
        [Route("ApproveMember")]
        public Task<HttpResponseMessage> ApproveMember(string email)
        {
            return _adminService.ApproveMemberAsync(email);

        }

        [HttpPut]
        [Route("ActivateMember")]
        public Task<HttpResponseMessage> ActivateMember(string email)
        {
            return _adminService.ActivateMemberAsync(email);

        }

    }
}