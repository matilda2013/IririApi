using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model.IService
{
    public interface IPaymentService
    {
        void AddPayment(PaymentGatewayStore eventTransaction, string email, string custName, string phoneNumber);


        List<PaymentGatewayStore> GetAllMemberPaymentsAsync();


    }
}
