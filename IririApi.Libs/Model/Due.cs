using IririApi.Libs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class Due 
    {
        [Key]
        public Guid DueId { get; set; }
        public string MembershipId { get; set; }
        public int NumberOfMonths { get; set; }

        public string Amount { get; set; }
        public DateTime DatePaid { get; set; }
        
    }
}
