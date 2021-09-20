using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.DTOs
{
    public class PayForEvent
    {
        public string EventPaymentPlanName { get; set; }
        public string email{ get; set; }
        public string paymentReference { get; set; }
        public decimal amount { get; set; }


    }
}
