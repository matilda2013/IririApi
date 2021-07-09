using Flurl;
using Flurl.Http;
using IririApi.Libs.DTOs;
using IririApi.Libs.Helpers;
using IririApi.Libs.Inteface.IService;
using IririApi.Libs.Model;
using IririApi.Libs.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static IririApi.Libs.Model.InitializeTransaction;

namespace IririApi.Controllers
{

      [Route("api/[controller]")]
      [ApiController]
    public class MemberDuePaymentController : Controller
    {
       
        private readonly IPaymentMemberDuesService _paymentService;
        Logger logger = LogManager.GetLogger("PayStackLogger");
        private UserManager<MemberRegistrationUser> _userManager;




        public MemberDuePaymentController(UserManager<MemberRegistrationUser> userManager, IPaymentMemberDuesService paymentService)
        {
                   this._userManager = userManager;

                    _paymentService = paymentService;

        }


        [HttpPost]
        [Route("PayMemberDues")]
   
   
        public async Task<ActionResult> PayStackGateway(MembershipPlanViewModel model)
        {

            try
            {

                decimal amount = model.amount;


                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var Username = await _userManager.FindByIdAsync(userId);
            
                var email = model.emailAddress;
                var phoneNumber = Username.PhoneNumber;
                var custName =  Username.FirstName + " " + Username.LastName;
                var MemberId = Username.Id;

            var callback = "https://localhost:44312/api/Payment/VerifyPaystackPayment";

                //PAYSTACK AMOUNT IS IN KOBO
                var requestObj = new { amount = (amount * 100).ToString(), email, callback };

                logger.Info("");
                logger.Info("------------------------------");
                logger.Info("initializing paystack transaction for - " + email + " , amount - " + amount);



                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11
                                                | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;




                var result = await ApiHelper.paystackUrl
                    .AppendPathSegment("transaction/initialize")
                    .WithOAuthBearerToken(ApiHelper.PaystackSecretKey)
                    .PostJsonAsync(requestObj)
                    .ReceiveJson<InitializeTransaction.InitializeTransactionResponseModel>();



                if (result.status)
                {
                    logger.Info("paystack initialisation was successful");

                    _paymentService.AddPayment(model, email, custName, phoneNumber, MemberId);

                    logger.Info("Callback Url - " + callback);
                    logger.Info("now redirecting to paystack site - " + result.data.authorization_url);

                    return Ok(new { url = result.data.authorization_url });

                }

                return RedirectToAction("");

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }




        }




        async Task<ActionResult> VerifyPaystackPayment(string reference)
        {
            var responseStatus = new TransactionStatusViewModel();
            responseStatus.paymentmethod = "Paystack";
            responseStatus.reference = reference;
            responseStatus.date = DateTime.Now;


            try
            {


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;


                var response = await ApiHelper.paystackUrl
                    .AppendPathSegment("transaction/verify/" + reference)
                    .WithOAuthBearerToken(ApiHelper.PaystackSecretKey)
                    .GetJsonAsync<InitializeTransaction.VerifyTransactionResponseModel>();

                if (response.status)
                {


                    if (response.data.status == "success")
                    {

                        var amount = ApiHelperFunctions.DeductPaystackCharges(response.data.amount / 100);

                        responseStatus.amount = amount;
                        responseStatus.status = response.status;

                    }

                    if (!string.IsNullOrEmpty(response.data.gateway_response))
                    {

                        responseStatus.reason = response.data.gateway_response;

                    }

                }


            }
            catch (Exception ex)
            {

                return null;
            }

            return null;
        }

        [HttpGet]
        [Route("ViewMemberDuesPaymentsById")]
        public DuePaymentTracker GetMemberDuePaymentByMemberId(string id)
        {
            return _paymentService.ViewDuePaymentsMemberByIdAsync(id);
        }


    }

}

