using IririApi.Libs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class EventPayment 
    {
        [Key]
        public Guid PayId { get; set; }
        public long EventId { get; set; }
        public Guid MemberId { get; set; }
      
        public DateTime EventDate { get; set; }
    }
}
