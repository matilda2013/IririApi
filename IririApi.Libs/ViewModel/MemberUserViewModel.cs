using IririApi.Libs.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.ViewModel
{
    public class MemberUserViewModel
    {
        public string MemId { get; set; }

        [Column(TypeName = "nvarchar(130)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(130)")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        public string MemberAddress { get; set; }

        public string MemberEmail { get; set; }

        public string MemberPhone { get; set; }

        public string Occupation { get; set; }

        public DateTime DOB { get; set; }

        public bool Status { get; set; }

        public string CardNo { get; set; }

        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "nvarchar(130)")]
        public string CreatedBy { get; set; }

        public bool IsPasswordChangeRequired { get; set; }

        public bool IsPasswordChanging { get; set; }

        public string Role { get; set; }

        public string Password { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal amount { get; set; }

        [Column(TypeName = "varchar(130)")]
        public string gateway_response { get; set; }

        public string PaymentMethod { get; set; }

        public string requestReference { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? paid_at { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime created_at { get; set; }


        [Column(TypeName = "varchar(130)")]
        public string currency { get; set; }
    }
}
