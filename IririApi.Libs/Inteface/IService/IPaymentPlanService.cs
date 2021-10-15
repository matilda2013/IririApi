using IririApi.Libs.Model;
using IririApi.Libs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Inteface.IService
{
   public  interface IPaymentPlanService
    {
       
       List<PaymentPlan> ViewAllPaymentPlanAsync();

        HttpResponseMessage AddPaymentPlanAsync(PaymentPlan model);

        HttpResponseMessage DeletePaymentPlanAsync(Guid id);
         List<PaymentPlan> GetPaymentById(Guid id);


        HttpResponseMessage UpdatePaymentPlanAsync(EditPlanViewModel payplanModel);
    }
}
