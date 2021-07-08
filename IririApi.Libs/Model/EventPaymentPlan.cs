using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class EventPaymentPlan
    {
        [Key]
        public Guid EventPaymentPlanId { get; set; }
        public string MembershipId { get; set; }
        public string PaymentPlanName { get; set; }
        public DateTime DatePaid { get; set; }
        public double amount { get; set; }
        public string Description { get; set; }

    }
}
