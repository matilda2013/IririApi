using AutoMapper;
using IririApi.Libs.Bootstrap.Exceptions;
using IririApi.Libs.Infrastructure.Concrete;
using IririApi.Libs.Infrastructure.Contract;
using IririApi.Libs.Model;
using IririApi.Libs.Model.IRepository;
using IririApi.Libs.Model.IService;
using IririApi.Libs.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Service
{
    public class AdminService : IAdminService
    {


        private readonly AuthenticationContext _DbContext;
        private readonly IRepository<MemberRegistrationUser> _adminrepository;
        private UserManager<MemberRegistrationUser> _userManager;

        public AdminService(AuthenticationContext DbContext, UserManager<MemberRegistrationUser> userManager)
        {
            _userManager = userManager;
            _DbContext = DbContext;
            _adminrepository = new Repository<MemberRegistrationUser>(DbContext);
        }

      


        public  async Task AddNewMemberAsync(MemberUserViewModel model)
        {
            model.Role = "Member";

            var MemberUser = new MemberRegistrationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.MemberEmail,
                MemberEmail = model.MemberEmail,
                Email = model.MemberEmail,
                MemberPhone = model.MemberPhone,
                Gender = model.Gender,
                MemberAddress = model.MemberAddress,
                Role = UserRole.MemberUser,
                DOB = model.DOB,
                Occupation = model.Occupation,
                CardNo = model.CardNo,
                Status = model.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = model.MemberEmail,
                IsPasswordChangeRequired = true,


            };

            try
            {
                var result = await _userManager.CreateAsync(MemberUser, model.Password);
                await _userManager.AddToRoleAsync(MemberUser, model.Role);

            }
            catch (Exception ex)
            {
                throw ex;
            }
      


        }

        public List<MemberRegistrationUser> GetAllMembersAsync()
        {

            var memList = _adminrepository.GetAll();
            return memList.ToList();


        }

        public HttpResponseMessage DeleteMemberAsync(string id)
        {
            try
            {


                MemberRegistrationUser myevent = _DbContext.MemberRegistrationUsers.FirstOrDefault(e => e.Id == id);

                if (myevent == null)
                {
                    throw new ObjectNotFoundException($"No Member With id{id} exists");
                }

                else
                {

                    _DbContext.MemberRegistrationUsers.Remove(myevent);
                    _DbContext.SaveChanges();
                    var response = new HttpResponseMessage();
                    response.Headers.Add("DeleteMessage", "Member Successfully Deleted!!!");
                    return response;

                }

            }

            catch (Exception ex)
            {
                throw ex;

            }


        }



    }
}
