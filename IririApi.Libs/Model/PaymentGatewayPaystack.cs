using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class PaymentGatewayPaystack
    {
      
            public string Amount { get; set; }
            public string CustomerEmail { get; set; }
            public string CallbackURL { get; set; }
            public string RequestReference { get; set; }
     }

        public class InitializeTransaction
        {
            public class InitializeTransactionRequestModel
            {
                public string callback_url { get; set; }
                public string reference { get; set; }
                public string email { get; set; }
                public string amount { get; set; }
            }

            


            public class InitializeTransactionResponseModel
            {
                public bool status { get; set; }
                public string message { get; set; }
                public Data data { get; set; }

            }

            public class Data
            {
                public string authorization_url { get; set; }
                public string access_code { get; set; }
                public string reference { get; set; }
                public decimal amount { get; set; }
                public string gateway_response { get; set; }
                public string status { get; set; }


            }

            public class VerifyTransactionResponseModel
            {
                public bool status { get; set; }
                public string message { get; set; }
                public Data data { get; set; }
            }

            public class TransactionStatusViewModel
            {
                public bool status { get; set; }
                public string reference { get; set; }
                public string reason { get; set; }
                public string amount { get; set; }
                public DateTime date { get; set; }
                public string paymentmethod { get; set; }
            }
        }
    }

