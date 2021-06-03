using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.ViewModel
{
    public class EventViewModel
    {
        public string EventTitle { get; set; }

        public DateTime Date { get; set; }

        public string EventVenue { get; set; }

        public string EventDescription { get; set; }

        public List<IFormFile> EventImage{ get; set; }

        public double Amount { get; set; }
    }
}
