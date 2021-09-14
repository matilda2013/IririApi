using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.ViewModel
{
    public class DueViewModel
    {
        public string MembershipId { get; set; }
        public int NumberOfMonths { get; set; }
        public string Amount { get; set; }
        public DateTime DatePaid { get; set; }
    }
}
