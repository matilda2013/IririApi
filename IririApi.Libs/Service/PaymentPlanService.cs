using IririApi.Libs.Bootstrap.Exceptions;
using IririApi.Libs.Infrastructure.Concrete;
using IririApi.Libs.Infrastructure.Contract;
using IririApi.Libs.Inteface.IService;
using IririApi.Libs.Model;
using IririApi.Libs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Service
{
    public class PaymentPlanService : IPaymentPlanService
    {

        private readonly AuthenticationContext _DbContext;
        private readonly IRepository<PaymentPlan> _paymentPlanrepository;
        public PaymentPlanService(AuthenticationContext DbContext)
        {

            _DbContext = DbContext;
            _paymentPlanrepository = new Repository<PaymentPlan>(DbContext);
           
        }


        public HttpResponseMessage UpdatePaymentPlanAsync(Guid id, PaymentPlanViewModel payplanModel)
        {
            try
            {


                PaymentPlan myplan = _DbContext.PaymentPlans.FirstOrDefault(e => e.PaymentPlanId == id);

                if (myplan == null)
                {
                    throw new ObjectNotFoundException($"No PaymentPlan With id{id} exists");
                }

                else
                {
                    myplan.PaymentPlanName = payplanModel.PaymentPlanName;
                    myplan.cost = payplanModel.cost;
                    myplan.Description = payplanModel.Description;
              

                    _DbContext.SaveChanges();
                    var response = new HttpResponseMessage();
                    response.Headers.Add("UpdateMessage", "Successfuly Updated!!!");
                    return response;



                }

            }

            catch (Exception ex)
            {
                throw ex;

            }


        }

        
        public HttpResponseMessage DeletePaymentPlanAsync(Guid id)
        {
            try
            {


              PaymentPlan myplan = _DbContext.PaymentPlans.FirstOrDefault(e => e.PaymentPlanId == id);

                if (myplan == null)
                {
                    throw new ObjectNotFoundException($"No PaymentPlan With id{id} exists");
                }

                else
                {

                    _DbContext.PaymentPlans.Remove(myplan);
                    _DbContext.SaveChanges();
                    var response = new HttpResponseMessage();
                    response.Headers.Add("DeleteMessage", "Successfuly Deleted!!!");
                    return response;

                }

            }

            catch (Exception ex)
            {
                throw ex;

            }


        }



        public List<PaymentPlan> ViewAllPaymentPlanAsync()
        {
            var payplanList = _paymentPlanrepository.GetAll();
            return payplanList.ToList();

        }

        
        public HttpResponseMessage AddPaymentPlanAsync(PaymentPlan model)
        {
            try
            {
                var result = new PaymentPlan();
                result.PaymentPlanId = model.PaymentPlanId;
                result.PaymentPlanName = model.PaymentPlanName;
                result.cost = model.cost;
                result.Description= model.Description;
             

                _DbContext.PaymentPlans.Add(result);
                _DbContext.SaveChanges();
                var response = new HttpResponseMessage();
                response.Headers.Add("CreatePaymentPlanMessage", "Successfuly Added!!!");
                return response;

            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}
