using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.ViewModel
{
    class PaymentGatewayStoreViewModel


    { 

    [Column(TypeName = "decimal(18,2)")]
    public decimal amount { get; set; }

    public string MemberName { get; set; }
    public string phoneNumber { get; set; }
    public string emailAddress { get; set; }
    public string requestReference { get; set; }

    public bool status { get; set; }

    public string PaymentMethod { get; set; }


    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    [Column(TypeName = "varchar(130)")]
    public string currency { get; set; }


    [Column(TypeName = "varchar(130)")]
    public string gateway_response { get; set; }

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
