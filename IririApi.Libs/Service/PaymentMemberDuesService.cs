using IririApi.Libs.Bootstrap.Exceptions;
using IririApi.Libs.DTOs;
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
    public class PaymentMemberDuesService : IPaymentMemberDuesService
    {
        private readonly AuthenticationContext _DbContext;
        private readonly IRepository<MembershipPlanPayment> _payrepository;

        public PaymentMemberDuesService(AuthenticationContext DbContext)
        {
            _DbContext = DbContext;
            _payrepository = new Repository<MembershipPlanPayment>(DbContext);
        }

        public void AddPayment(MembershipPlanViewModel dueTransaction, string email, string custName, string phoneNumber, string MemberId)
        {
            try
            {
                var result = new MembershipPlanPayment();

            
                result.MemberName = custName;
                result.MembershipId = MemberId;
                result.phoneNumber = phoneNumber;
                result.amount = dueTransaction.amount;
                result.emailAddress = email;
      
                result.PaymentPlanName =  dueTransaction.PaymentPlanName;
                result.paymentReference =  dueTransaction.paymentReference;
                result.paid_at = DateTime.Now;
                result.created_at = DateTime.Now;
              


                _DbContext.MembershipPlanPayments.Add(result);
                _DbContext.SaveChanges();
                //send email to the user that his payment has been approved

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public DuePaymentTracker ViewDuePaymentsMemberByIdAsync(string id)
        {


            try
            {
                MembershipPlanPayment myevent =  _DbContext.MembershipPlanPayments.FirstOrDefault(e => e.MembershipId == id);


                if (myevent == null)
                {
                    throw new ObjectNotFoundException($"Member With id{id} has not made payment");


                }

                var eventresult = new DuePaymentTracker()
                    {
                        PaymentPlanName = myevent.PaymentPlanName,
                        MemberName = myevent.MemberName,
                        amount = myevent.amount,
                        paymentReference = myevent.paymentReference,
                        DatePaid = myevent.DatePaid


                    };
             


                return eventresult;


                
            }

            catch (Exception ex)
            {
                
                throw ex;

            }


        }

    }
}
