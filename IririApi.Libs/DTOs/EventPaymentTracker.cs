using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.DTOs
{
    public class EventPaymentTracker
    {
        public string MemberName { get; set; }
        public string EventPaymentPlanName { get; set; }
        public string emailAddress { get; set; }
        public DateTime DatePaid { get; set; }

        public decimal amount { get; set; }
        public string Description { get; set; }

        public string phoneNumber { get; set; }


        public string paymentReference { get; set; }
    }
}
