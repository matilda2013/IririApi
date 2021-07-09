using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class MembershipPlanPayment
    {
        [Key]
        public Guid MemDuePaymentPlanId { get; set; }
        public string MembershipId { get; set; }

        public string MemberName { get; set; }
        public string PaymentPlanName { get; set; }
        public string emailAddress { get; set; }

        public DateTime DatePaid { get; set; }
        public decimal amount { get; set; }
        public string Description { get; set; }

        public string phoneNumber { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Column(TypeName = "varchar(130)")]
        public string currency { get; set; }

        public string paymentReference { get; set; }

        [Column(TypeName = "varchar(130)")]
        public string gateway_response { get; set; }

        public bool status { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? paid_at { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime created_at { get; set; }
    }
}
