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
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IririApi.Libs.Service
{
    public class AdminService : IAdminService
    {


        private readonly AuthenticationContext _DbContext;
        private readonly IRepository<MemberRegistrationUser> _adminrepository;
        private UserManager<MemberRegistrationUser> _userManager;
        string url1 = "https://localhost:44312";

        public AdminService(AuthenticationContext DbContext, UserManager<MemberRegistrationUser> userManager)
        {
            _userManager = userManager;
            _DbContext = DbContext;
            _adminrepository = new Repository<MemberRegistrationUser>(DbContext);
        }

      


        public  async Task AddNewMemberAsync(MemberUserViewModel model)
        {
          //  model.Role = "Admin";

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
               // CardNo = model.CardNo,
               // Status = model.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = model.MemberEmail,
                IsPasswordChangeRequired = true,


            };

            try
            {
                var result = await _userManager.CreateAsync(MemberUser, model.Password);
                await _userManager.AddToRoleAsync(MemberUser, "Admin");

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




        public List<MemberRegistrationUser> GetPendingRegistrationsAsync()
        {
            var memList = _adminrepository.GetAll(x => x.Status == "Pending");
            return memList.ToList();
        }

        public async Task<bool> SendMail(string email, string subject, string body)
        {
            try
            {
               

                var emailCredEmail = "membership@iriridc.com";


                var mailFrom = new MailAddress(emailCredEmail, "IririDC");
                var mailTo = new MailAddress(email);

                MailMessage mailMsg = new MailMessage(mailFrom, mailTo);
                mailMsg.Subject = subject;

                mailMsg.Body = body;

                mailMsg.IsBodyHtml = true;
                var SmtpHost = "mail.iriridc.com";//"smtp.1and1.com";
                int SmtpPort = 8889;// 587;
                SmtpClient smtp = new SmtpClient(SmtpHost, SmtpPort);
                smtp.Credentials = new NetworkCredential(emailCredEmail, "Password1@");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mailMsg);

          
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<HttpResponseMessage> ApproveMemberAsync(string email)
        {
            //var member = _adminrepository.GetById(email);

            var member= await _userManager.FindByNameAsync(email);
          
            var name = member.FirstName + " " + member.LastName;
            var plan = member.Plan;
            var amount = member.Amount;
        
            member.Status = "Approved";

            //Set Status to Approve and Send Mail Containing the Plan and Payment Information
           await _userManager.UpdateAsync(member);
            var response = new HttpResponseMessage();
            response.Headers.Add("Approval", "Member Successfully Approved!!!");

            //Send Mail Containing Plan and Amount
            string body = "Dear " + name + ", your registration request has been approved. Kindly complete your registration by paying the sum of " + amount + " for " + plan + " subscription plan with the link below \n \n";
            body = body + "https://iriridc.com/subpay?email=" + email + "&plan=" + plan + "&amount=" + amount + "&name=" + name;

            await SendMail(email, "Payment Approval", body);
            return response;

        }

        public async Task<bool> SendConfirmRegistrationMail(string userEmail)
        {
            var user = await _userManager.FindByNameAsync(userEmail);
            var Id = user.Id;


            var mytoken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string mytokenVersion = HttpUtility.UrlEncode(mytoken);




            var callback = url1 + "/api/MemberUser/ConfirmEmail?Id=" + Id + "&token=" + mytokenVersion; 


            var body = "";

            using (var myReader = System.IO.File.OpenText(@"C:\inetpub\wwwroot\IRIRIregistrationEmail.txt"))
            {

                body = myReader.ReadToEnd();

            }

            body = body.Replace("{Firstname}", user.FirstName);
            body = body.Replace("{link}", callback);
            body = body.Replace("{Email}", user.Email);

            var subject = "Email Confirmation";
            var successMessage = "Please check your email to confirm your account";

            var res = await SendMail(user.Email, subject, body);

            if (res)
            {

                var result = successMessage;

                return true;
            }
            return res;


        }

        public async Task<HttpResponseMessage> ActivateMemberAsync(string email)
        {
            dynamic MemberUser = await _userManager.FindByNameAsync(email);
            MemberUser.Status = "Active";
            await _userManager.UpdateAsync(MemberUser);


            dynamic password = new Guid().ToString().Substring(1, 6);
           dynamic randpass = password;
            MemberUser.randPassword = randpass;
            //Save Credentials on Identity
            try
            {
               // var result = await _userManager.CreateAsync(MemberUser, password);
               var result = await _userManager.UpdateAsync(MemberUser);
                await _userManager.AddToRoleAsync(MemberUser, "Member");
                // await _userManager.AddToRoleAsync(MemberUser, "Member");

                var resp = await SendConfirmRegistrationMail(MemberUser.Email);

                if (!resp)
                {

                    throw new ObjectNotFoundException($"Couldn't Send Confirmation Email. Attempt to Login to resend confirmation link");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //send members Credentials

            var response = new HttpResponseMessage();
            response.Headers.Add("Approval", "Member Successfully Approved!!!");
            return response;
        }



    }
}
