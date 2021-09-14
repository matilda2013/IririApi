using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class EmailLog
    {
        [Column(TypeName = "bigint")]
        public long Id { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }

        public string RecipientEmailAddress { get; set; }
        public int RetryCount { get; set; }
        public bool IsSent { get; set; }
        public DateTime TimeEntered { get; set; }
        public DateTime? TimeSent { get; set; }

    }
}
