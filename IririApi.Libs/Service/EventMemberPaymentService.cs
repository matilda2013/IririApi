using IririApi.Libs.DTOs;
using IririApi.Libs.Infrastructure.Concrete;
using IririApi.Libs.Infrastructure.Contract;
using IririApi.Libs.Inteface.IService;
using IririApi.Libs.Model;
using IririApi.Libs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Service
{
    public class EventMemberPaymentService : IEventPaymentDuesService
    {

        private readonly AuthenticationContext _DbContext;
        private readonly IRepository<EventPaymentPlan> _payrepository;

        public EventMemberPaymentService(AuthenticationContext DbContext)
        {
            _DbContext = DbContext;
            _payrepository = new Repository<EventPaymentPlan>(DbContext);
        }

        public void AddPayment(PayForEvent eventTransaction, string email, string custName, string phoneNumber, string MemberId)
        {
           
            try
            {
              
                var result = new EventPaymentPlan();

                result.MembershipId = MemberId;
                result.MemberName = custName;
                result.phoneNumber = phoneNumber;
                result.amount = eventTransaction.amount;
                result.emailAddress = email;
             
                result.PaymentPlanName = eventTransaction.EventPaymentPlanName;
                result.paymentReference = eventTransaction.paymentReference;
                result.paid_at = DateTime.Now;
                result.created_at = DateTime.Now;
         
        

                _DbContext.EventPaymentPlans.Add(result);
                _DbContext.SaveChanges();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public EventPaymentTracker ViewEventPaymentsByIdAsync(string id)
        {


            EventPaymentPlan myevent = _DbContext.EventPaymentPlans.FirstOrDefault(e => e.MembershipId == id);
     

            var eventresult = new EventPaymentTracker()
            {
                EventPaymentPlanName = myevent.EventPaymentPlanName,
                MemberName = myevent.MemberName,
                amount = myevent.amount,
                paymentReference = myevent.paymentReference,
                DatePaid = myevent.DatePaid


            };


            return eventresult;

        }
    }
}

