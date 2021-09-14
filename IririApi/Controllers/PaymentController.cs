//using Flurl;
//using Flurl.Http;
//using IririApi.Libs.Helpers;
//using IririApi.Libs.Model;
//using IririApi.Libs.Model.IService;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using NLog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using static IririApi.Libs.Model.InitializeTransaction;

//namespace IririApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PaymentController : ControllerBase
//    {

//        private readonly IPaymentService _paymentService;
//        Logger logger = LogManager.GetLogger("PayStackLogger");
//        private UserManager<MemberRegistrationUser> _userManager;


//        public PaymentController(UserManager<MemberRegistrationUser> userManager, IPaymentService paymentService)
//        {
//            this._userManager = userManager;

//            _paymentService = paymentService;

//        }


//        [HttpPost]
//        [Route("PaymentApi")]

//        public async Task<ActionResult> PaymentApi(PaymentGatewayStore model)
//        {

//            switch (model.PaymentMethod)
//            {
//                case "paystack":
//                    return await PayStackGateway(model);

//                case "Flutterwave":
//                //return await FlutterWave(model);


//                default:
//                    return null;
//            }



//            [Route("PayStackGateway")]

//            async Task<ActionResult> PayStackGateway(PaymentGatewayStore model)
//            {

//                try
//                {

//                    decimal amount = model.amount;


//                    string userId = User.Claims.First(c => c.Type == "UserID").Value;
//                    var Username = await _userManager.FindByIdAsync(userId);
//                    var email =  Username.Email;
//                    var phoneNumber =Username.PhoneNumber;
//                    var custName = Username.FirstName + " " + Username.LastName;

//                    var callback = "https://localhost:44312/api/Payment/VerifyPaystackPayment";

//                    //PAYSTACK AMOUNT IS IN KOBO
//                    var requestObj = new { amount = (amount * 100).ToString(), email, callback };

//                    logger.Info("");
//                    logger.Info("------------------------------");
//                    logger.Info("initializing paystack transaction for - " + email + " , amount - " + amount);



//                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11
//                                                    | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;




//                    var result = await ApiHelper.paystackUrl
//                        .AppendPathSegment("transaction/initialize")
//                        .WithOAuthBearerToken(ApiHelper.PaystackSecretKey)
//                        .PostJsonAsync(requestObj)
//                        .ReceiveJson<InitializeTransaction.InitializeTransactionResponseModel>();



//                    if (result.status)
//                    {
//                        logger.Info("paystack initialisation was successful");

//                        _paymentService.AddPayment(model, email, custName, phoneNumber);

//                        logger.Info("Callback Url - " + callback);
//                        logger.Info("now redirecting to paystack site - " + result.data.authorization_url);

//                        return Ok(new { url = result.data.authorization_url });

//                    }

//                    return RedirectToAction("");

//                }
//                catch (Exception ex)
//                {
//                    logger.Error(ex.Message);
//                    return null;
//                }

//            }

//        }

//          async Task<ActionResult> VerifyPaystackPayment(string reference)
//          {
//            var responseStatus = new TransactionStatusViewModel();
//            responseStatus.paymentmethod = "Paystack";
//            responseStatus.reference = reference;
//            responseStatus.date = DateTime.Now;


//            try
//            {


//                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;


//                var response = await ApiHelper.paystackUrl
//                    .AppendPathSegment("transaction/verify/" + reference)
//                    .WithOAuthBearerToken(ApiHelper.PaystackSecretKey)
//                    .GetJsonAsync<InitializeTransaction.VerifyTransactionResponseModel>();

//                if (response.status)
//                {


//                    if (response.data.status == "success")
//                    {

//                        var amount = ApiHelperFunctions.DeductPaystackCharges(response.data.amount / 100);

//                        responseStatus.amount = amount;
//                        responseStatus.status = response.status;

//                    }

//                    if (!string.IsNullOrEmpty(response.data.gateway_response))
//                    {

//                        responseStatus.reason = response.data.gateway_response;

//                    }

//                }


//            }
//            catch (Exception ex)
//            {

//                return null;
//            }

//            return null;
//        }

    
//        [HttpGet]
//        [Route("ViewMemberPaymentHistory")]
//        public List<PaymentGatewayStore> ViewMemberPaymentHistory()
//        {
//            return _paymentService.GetAllMemberPaymentsAsync();
//        }


//    }
//}
