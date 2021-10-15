using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.ViewModel
{
  public class EditPlanViewModel
    {
        public Guid PaymentPlanId { get; set; }
        public string PaymentPlanName { get; set; }
        public double cost { get; set; }
        public string Description { get; set; }
    }
}
