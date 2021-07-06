using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class PaymentPlan
    {
        [Key]
        public Guid PaymentPlanId { get; set; }
        public string PaymentPlanName { get; set; }
        public double  cost{ get; set; }
        public string Description { get; set; }
       
    }
}
