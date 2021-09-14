using IririApi.Libs.DTOs;
using IririApi.Libs.Model;
using IririApi.Libs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Inteface.IService
{
    public interface IPaymentMemberDuesService
    {
        void AddPayment(MembershipPlanViewModel eventTransaction, string email, string custName, string phoneNumber, string MemberId);

        DuePaymentTracker ViewDuePaymentsMemberByIdAsync(string id);
      
    }
}
