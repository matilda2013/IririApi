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
       // public string MemId { get; set; }

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

 

        public string Password { get; set; }


      
    }
}
