using IririApi.Libs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class Preference 
    {
        [Key]
        public Guid PreferId { get; set; }

        public Guid MemberId { get; set; }
        public string DuesPaymentPreferenceType { get; set; }

        
    }
}
