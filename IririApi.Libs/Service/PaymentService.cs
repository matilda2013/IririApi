using IririApi.Libs.Helpers;
using IririApi.Libs.Infrastructure.Concrete;
using IririApi.Libs.Infrastructure.Contract;
using IririApi.Libs.Model;
using IririApi.Libs.Model.IService;
using IririApi.Libs.ViewModel;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using static IririApi.Libs.Model.InitializeTransaction;

namespace IririApi.Libs.Service
{
    public class PaymentService : IPaymentService
    {

        private readonly AuthenticationContext _DbContext;
        private readonly IRepository<PaymentGatewayStore> _payrepository;

        public PaymentService(AuthenticationContext DbContext)
        {
            _DbContext = DbContext;
            _payrepository = new Repository<PaymentGatewayStore>(DbContext);
        }

        public void AddPayment(PaymentGatewayStore eventTransaction, string email, string custName, string phoneNumber)
        {
            try
            {
                var result = new PaymentGatewayStore();

                result.PaymentId = Guid.NewGuid(); 
                result.MemberName = custName;
                result.phoneNumber = phoneNumber;
                result.amount = eventTransaction.amount;
                result.emailAddress = email;
                result.gateway_response = eventTransaction.gateway_response;
                result.PaymentMethod = eventTransaction.PaymentMethod;
                result.PaymentPlanName = eventTransaction.PaymentPlanName;
                result.requestReference = eventTransaction.requestReference;
                result.paid_at = eventTransaction.paid_at;
                result.created_at = eventTransaction.created_at;
                result.currency = eventTransaction.currency;
                result.TotalPrice = eventTransaction.TotalPrice;
                result.status = eventTransaction.status;


                _DbContext.PaymentGatewayStores.Add(result);
                _DbContext.SaveChanges();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public List<PaymentGatewayStore> GetAllMemberPaymentsAsync()
        {

            var paymentList = _payrepository.GetAll();
            return paymentList.ToList();


        }

    }



    
}
