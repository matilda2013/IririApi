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
    public class EventPaymentController : Controller
    {
            private readonly IEventPaymentDuesService _paymentService;
            Logger logger = LogManager.GetLogger("PayStackLogger");
            private UserManager<MemberRegistrationUser> _userManager;






        public EventPaymentController(UserManager<MemberRegistrationUser> userManager, IEventPaymentDuesService paymentService)
        {
            this._userManager = userManager;

            _paymentService = paymentService;

        }

        [HttpPost]
       
        [Route("PayEventDues")]

        public async Task<ActionResult> PayStackGateway(PayForEvent model)
        {

                try
                {
                var Username = await _userManager.FindByEmailAsync(model.email);

                var email = model.email;
                var custName = Username.FirstName + " " + Username.LastName;
                var phone = Username.MemberPhone;
                var MemberId = Username.Id;

                 _paymentService.AddPayment(model, email, custName, phone, MemberId);

                return Ok();

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
        [Route("ViewMemberEventPaymentsById")]
        public EventPaymentTracker GetEventsPaymentById(string id)
        {
            return _paymentService.ViewEventPaymentsByIdAsync(id);
        }

    }



}
