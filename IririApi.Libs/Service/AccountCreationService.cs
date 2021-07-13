using IririApi.Libs.Bootstrap.Exceptions;
using IririApi.Libs.DTOs;
using IririApi.Libs.Infrastructure.Concrete;
using IririApi.Libs.Infrastructure.Contract;
using IririApi.Libs.Inteface.IService;
using IririApi.Libs.Model;
using IririApi.Libs.Model.IService;
using IririApi.Libs.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class AccountCreationService : IUserAccountService
    {
        private readonly AuthenticationContext _DbContext;
        private UserManager<MemberRegistrationUser> _userManager;
        private readonly IRepository<EmailLog> _emailrepository;
        string url1 = "https://localhost:44312";

        public AccountCreationService(UserManager<MemberRegistrationUser> userManager, AuthenticationContext DbContext)
        {
            _userManager = userManager;
            _DbContext = DbContext;
            _emailrepository = new Repository<EmailLog>(DbContext);
        }
        public async Task RegisterAdminUserAsync(MemberUserViewModel model)
        {
           // model.Role = "Admin";

            var MemberUser = new MemberRegistrationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.MemberEmail,
                MemberEmail = model.MemberEmail,
                Email = model.MemberEmail,
                //MemberId = Guid.NewGuid(),
                MemberPhone = model.MemberPhone,
                Gender = model.Gender,
                MemberAddress = model.MemberAddress,
                Role = UserRole.Admin,
                DOB = model.DOB,
                Occupation = model.Occupation,
                //CardNo = model.CardNo,
              //  Status = model.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = model.MemberEmail,
                IsPasswordChangeRequired = true,


            };

            try
            {
                var result = await _userManager.CreateAsync(MemberUser, model.Password);
                await _userManager.AddToRoleAsync(MemberUser, "Admin");
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
        }



        public async Task RegisterMemberUserAsync(MemberUserViewModel model)
        {
          //  model.Role = "Member";

            var MemberUser = new MemberRegistrationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.MemberEmail,
                MemberEmail = model.MemberEmail,
                Email = model.MemberEmail,
                //MemberId = Guid.NewGuid(),
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
               // await _userManager.AddToRoleAsync(MemberUser, model.Role);
                await _userManager.AddToRoleAsync(MemberUser, "Member");
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
        }


        public MemberUserTracker ViewMembersByIdAsync(string userEmail)
        {
         

            MemberRegistrationUser myMemberCard = _DbContext.MemberRegistrationUsers.FirstOrDefault(e => e.Email == userEmail);

            var user = new MemberUserTracker()
            {
                //MemId = myMemberCard.Id,
                FirstName = myMemberCard.FirstName,
                LastName = myMemberCard.LastName,
                MemberEmail = myMemberCard.MemberEmail,
                MemberAddress = myMemberCard.MemberAddress,
                MemberPhone = myMemberCard.MemberPhone,
                Gender = myMemberCard.Gender,
                Occupation = myMemberCard.Occupation,
                DOB = myMemberCard.DOB,
                CardNo= myMemberCard.CardNo,
                CreatedAt =myMemberCard.CreatedAt,
                CreatedBy=myMemberCard.CreatedBy
                


            };


            return user;

        }

        public MemberUserTracker ViewMembersByCardNoAsync(string id)
        {
            MemberRegistrationUser myMemberCard = _DbContext.MemberRegistrationUsers.FirstOrDefault(e => e.CardNo == id);

            var user = new MemberUserTracker()
            {
                //MemId = myMemberCard.Id,
                FirstName = myMemberCard.FirstName,
                LastName = myMemberCard.LastName,
                MemberEmail = myMemberCard.MemberEmail,
                MemberAddress = myMemberCard.MemberAddress,
                MemberPhone = myMemberCard.MemberPhone,
                Gender = myMemberCard.Gender,
                Occupation = myMemberCard.Occupation,
                DOB = myMemberCard.DOB,
                CardNo = myMemberCard.CardNo,
                CreatedAt = myMemberCard.CreatedAt,
                CreatedBy = myMemberCard.CreatedBy



            };

  
            return user;

        }

       public async Task<HttpResponseMessage> TieMembersByCardNoAsync(string userEmail, string CardNo)
       
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userEmail);
                //MemberRegistrationUser myMember1 = _DbContext.MemberRegistrationUsers.FirstOrDefault(e => e.Email == userEmail);

              //  var memId = myMember1.Id;
                var memId = user.Id;
                

                MemberRegistrationUser myMember = _DbContext.MemberRegistrationUsers.FirstOrDefault(e => e.Id == memId);

                if (myMember == null)
                {
                    throw new ObjectNotFoundException($"No Member With id{memId} exists");
                }

                else
                {
                    myMember.CardNo = CardNo;
                   

                    _DbContext.SaveChanges();
                    var response = new HttpResponseMessage();
                    response.Headers.Add("UpdateMessage", "Successfully Updated!!!");
                    return response;


                }

            }

            catch (Exception ex)
            {
                throw ex;

            }

        

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



        public async Task<bool> SendMail(string email, string subject, string body)
        {
            try
            {
                var emailId = InsertEmailLog(body, email, subject);

                var emailCredEmail = "no-reply@epayplusng.com";

                var password = "UGFzc3dvcmQx";

                var passwordenc = System.Convert.FromBase64String(password);
                var passResult = System.Text.Encoding.UTF8.GetString(passwordenc);

                var mailFrom = new MailAddress(emailCredEmail, "EpayplusNg");
                var mailTo = new MailAddress(email);

                MailMessage mailMsg = new MailMessage(mailFrom, mailTo);
                mailMsg.Subject = subject;

                mailMsg.Body = body;

                mailMsg.IsBodyHtml = true;
                var SmtpHost = "smtp.1and1.com";
                int SmtpPort = 587;
                SmtpClient smtp = new SmtpClient(SmtpHost, SmtpPort);
                smtp.Credentials = new NetworkCredential(emailCredEmail, passResult);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mailMsg);

                new Task(() =>
                {
                    UpdateEmailLog(emailId);

                }).Start();


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public long InsertEmailLog(string emailMsg, string receipientEmail, string subject)
        {


            try
            {
                long item = AddEmaillog(emailMsg, receipientEmail, subject);
             
                return item;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void UpdateEmailLog(long Id)
        {
            var email = _DbContext.EmailLogs.Where(a => a.Id == Id).FirstOrDefault();
            email.IsSent = true;
            email.TimeSent = DateTime.Now;
            _DbContext.SaveChanges();

        }



        public long AddEmaillog(string emailMsg, string receipientEmail, string subject)
        {
            var emailLog = new EmailLog()
            {
                EmailMessage = emailMsg,
                EmailSubject = subject,
                IsSent = false,
                RecipientEmailAddress = receipientEmail,
                RetryCount = 0,
                TimeEntered = DateTime.Now
            };

            _DbContext.EmailLogs.Add(emailLog);
            _DbContext.SaveChanges();
             return emailLog.Id;
        }


    }
}
