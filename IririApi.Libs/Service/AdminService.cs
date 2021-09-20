using AutoMapper;
using IririApi.Libs.Bootstrap.Exceptions;
using IririApi.Libs.Infrastructure.Concrete;
using IririApi.Libs.Infrastructure.Contract;
using IririApi.Libs.Model;
using IririApi.Libs.Model.IRepository;
using IririApi.Libs.Model.IService;
using IririApi.Libs.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
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
        private IHostingEnvironment _env;

        public AdminService(AuthenticationContext DbContext, UserManager<MemberRegistrationUser> userManager, IHostingEnvironment env)
        {
            _userManager = userManager;
            _DbContext = DbContext;
            _adminrepository = new Repository<MemberRegistrationUser>(DbContext);
            _env = env;
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
               Status = "Pending",
                CreatedAt = DateTime.Now,
                CreatedBy = model.MemberEmail,
                IsPasswordChangeRequired = true,


            };

            try
            {
                var result = await _userManager.CreateAsync(MemberUser);
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



        public List<MemberRegistrationUser> GetActiveMemberAsync()
        {
            var memList = _adminrepository.GetAll(x => x.Status == "Approved");
            return memList.ToList();
        }

        public async Task<bool> SendMail(string email, string subject, string body)
        {
            try
            {
                      

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
                var SmtpHost = "smtp.1and1.com";//"smtp.1and1.com";
                int SmtpPort = 587;// 587;
                SmtpClient smtp = new SmtpClient(SmtpHost, SmtpPort);
                smtp.Credentials = new NetworkCredential(emailCredEmail, passResult);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mailMsg);

                //here

                //var emailCredEmail = "no-reply@epayplusng.com";

                //var password = "UGFzc3dvcmQx";

                //var passwordenc = System.Convert.FromBase64String(password);
                //var passResult = System.Text.Encoding.UTF8.GetString(passwordenc);

                //var mailFrom = new MailAddress(emailCredEmail, "EpayplusNg");
                //var mailTo = new MailAddress(email);

                //MailMessage mailMsg = new MailMessage(mailFrom, mailTo);
                //mailMsg.Subject = subject;

                //mailMsg.Body = body;

                //mailMsg.IsBodyHtml = true;
                //var SmtpHost = "smtp.1and1.com";
                //int SmtpPort = 587;
                //SmtpClient smtp = new SmtpClient(SmtpHost, SmtpPort);
                //smtp.Credentials = new NetworkCredential(emailCredEmail, passResult);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static RestResponse SendApproveMessage(string body, string email)
        {
            try
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator =
                new HttpBasicAuthenticator("api",
                                           "ebc621b4d61b466ba6af2ec24cc96b93-9ad3eb61-7e11e71f");
                RestRequest request = new RestRequest();
                request.AddParameter("domain", "sandbox2b85528391224c619ffa6313a71be78c.mailgun.org", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "Iriri <testEmail@iriridc.com>");
                request.AddParameter("to", email);
                request.AddParameter("subject", "PAYMENT APPROVAL");
                request.AddParameter("text", body);
                request.Method = Method.POST;
                return (RestResponse)client.Execute(request);
            }
            catch (Exception ex)
            {
                var err = ex.Message + " " + ex.InnerException;
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
            body = body + "https://localhost:44313/Paystack?email=" + email; //+ "&plan=" + plan + "&amount=" + amount + "&name=" + name;

             SendApproveMessage(email,  body);
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
        public async Task<bool> SendMail2(string email, string subject, string body)
        {
            try
            {
                //SendSimpleMessage();

                MailMessage mail = new MailMessage();

                //set the addresses 
                mail.From = new MailAddress("testEmail@iriridc.com"); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add(email);

                //set the content 
                mail.Subject = subject;
                mail.Body = body;
                //send the message 
                SmtpClient smtp = new SmtpClient("mail.iriridc.com");
                

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("testEmail@iriridc.com", "password1@");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Port = 8889;    //alternative port number is 8889
                smtp.EnableSsl = false;
                smtp.Send(mail);
                //here

                //var emailCredEmail = "no-reply@epayplusng.com";

                //var password = "UGFzc3dvcmQx";

                //var passwordenc = System.Convert.FromBase64String(password);
                //var passResult = System.Text.Encoding.UTF8.GetString(passwordenc);

                //var mailFrom = new MailAddress(emailCredEmail, "EpayplusNg");
                //var mailTo = new MailAddress(email);

                //MailMessage mailMsg = new MailMessage(mailFrom, mailTo);
                //mailMsg.Subject = subject;

                //mailMsg.Body = body;

                //mailMsg.IsBodyHtml = true;
                //var SmtpHost = "smtp.1and1.com";
                //int SmtpPort = 587;
                //SmtpClient smtp = new SmtpClient(SmtpHost, SmtpPort);
                //smtp.Credentials = new NetworkCredential(emailCredEmail, passResult);
                return true;
            }
            catch (Exception ex)
            {
                SmtpException exw =  new SmtpException();
                var rrr = exw.InnerException + exw.Message;
               // var rrr = ex.InnerException + " " + ex.Message;
                throw ex;
            }

        }



        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    //create the mail message 
        //    MailMessage mail = new MailMessage();

        //    //set the addresses 
        //    mail.From = new MailAddress("postmaster@yourdomain.com"); //IMPORTANT: This must be same as your smtp authentication address.
        //    mail.To.Add("postmaster@yourdomain.com");

        //    //set the content 
        //    mail.Subject = "This is an email";
        //    mail.Body = "This is from system.net.mail using C sharp with smtp authentication.";
        //    //send the message 
        //    SmtpClient smtp = new SmtpClient("mail.yourdomain.com");

        //    //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
        //    NetworkCredential Credentials = new NetworkCredential("postmaster@yourdomain.com", "password");
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = Credentials;
        //    smtp.Port = 25;    //alternative port number is 8889
        //    smtp.EnableSsl = false;
        //    smtp.Send(mail);
        //   // lblMessage.Text = "Mail Sent";
        //}




        public string GeneratePassword()
        {
            Random r = new Random();
            var x = r.Next(0, 1000000);
            var randomNumber = x.ToString("000000");
            return randomNumber;
        }







        public async Task<HttpResponseMessage> ActivateMemberAsync(string email)
        {
            dynamic MemberUser = await _userManager.FindByNameAsync(email);
            MemberUser.Status = "Active";
            MemberUser.EmailConfirmed = true;

            dynamic oldpass = "Password1";
            dynamic randpass = GeneratePassword();
            var mycode = await _userManager.GeneratePasswordResetTokenAsync(MemberUser);
            await _userManager.ResetPasswordAsync(MemberUser, mycode, randpass);
            // await _userManager.ChangePasswordAsync(MemberUser,oldpass,randpass);
            //var newPasswordHash = this.PasswordHasher.HashPassword(newPassword);
            await _userManager.UpdateAsync(MemberUser);







            //Save Credentials on Identity
            try
            {
                // var result = await _userManager.CreateAsync(MemberUser, password);
                var result = await _userManager.UpdateAsync(MemberUser);

                if (result.Succeeded)
                {

                }
                await _userManager.AddToRoleAsync(MemberUser, "Member");
                //send mail here

                var pathToFile = _env.WebRootPath
                           + Path.DirectorySeparatorChar.ToString()
                           + "Template"
                           + Path.DirectorySeparatorChar.ToString()
                           + "EmailTemplate"
                           + Path.DirectorySeparatorChar.ToString()
                           + "Welcome_Email_Template.html";

                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }
                var subject = "Confirm Account Registration";
                string Message = ($" Dear {email}, Kindly login with your email and this password {randpass}");

                string messageBody = string.Format(builder.HtmlBody,
                            subject,
                            String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                            email,
                           email,
                            randpass,
                            Message,
                            Message
                            );



                SendSimpleMessage(messageBody, email);


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

        public void  SendSimpleMessage(string body, string email)
        {
            try
            {
                MailMessage mail = new MailMessage();

                //set the addresses 
                mail.From = new MailAddress("developer@iriridc.com"); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add(email);

                //set the content 
                mail.Subject = "This is an email";
                mail.Body = body;
                mail.IsBodyHtml = true;
                //send the message 
                SmtpClient smtp = new SmtpClient("mail5015.site4now.net");

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("developer@iriridc.com", "Password1@");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Port = 25;    //alternative port number is 8889
                smtp.EnableSsl = false;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                var err = ex.Message + " " + ex.InnerException;
                throw ex;
            }
           
        }

    }
}
