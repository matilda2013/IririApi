using IririApi.Libs.Inteface.IService;
using IririApi.Libs.Model;
using IririApi.Libs.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IririApi.Controllers
{
    public class PaymentPlanController : Controller
    {
       

        private readonly IPaymentPlanService _payplanService;
      
        public PaymentPlanController(IPaymentPlanService payplanService)
        {
            _payplanService = payplanService;
          
        }

        [HttpGet]
        [Route("ViewPaymentPlans")]
        public List<PaymentPlan> ViewPaymentPlan()
        {
            return _payplanService.ViewAllPaymentPlanAsync();
        }

        [HttpPost]
        [Route("AddPaymentPlan")]
        public HttpResponseMessage AddPaymentPlan([FromBody] PaymentPlan model)
        {
           

            return _payplanService.AddPaymentPlanAsync(model);
        }

        
        [HttpPut]
        [Route("EditPaymentPlan")]
        public HttpResponseMessage UpdateEventDue(Guid id, PaymentPlanViewModel model)
        {
            return _payplanService.UpdatePaymentPlanAsync(id, model);

        }

        [HttpDelete]
        [Route("DeletePaymentPlan")]
        public HttpResponseMessage DeletePaymentPlan(Guid id)
        {
            return _payplanService.DeletePaymentPlanAsync(id);

        }




    }
}
