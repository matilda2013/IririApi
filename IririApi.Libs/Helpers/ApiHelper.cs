using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Helpers
{
    public class ApiHelper
    {
        //PAYSTACK 

        public static string paystackUrl = "https://api.paystack.co/";
        public static string PaystackTestSecretKey = "sk_test_63df68ec0d9d563c21df9bd88e9d65c56087c522";
        public static string PaystackLiveSecretKey = "pk_test_2fe9603a986f09880f1a4cfc1d8aeb278fa0eb34";
        
        public static string PaystackSecretKey = PaystackTestSecretKey;
    }
}
