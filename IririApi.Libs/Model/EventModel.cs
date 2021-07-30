using IririApi.Libs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class EventModel 
    {
        [Key]
        public Guid EventId { get; set; }

        public string EventTitle{ get; set; }
        public string EventBase64 { get; set; }
        public DateTime Date { get; set; }

        public string EventVenue { get; set; }


        public string EventDescription { get; set; }

        public string EventPicture{ get; set; }

        public double Amount { get; set; }

        public bool status { get; set; }


    }
}
