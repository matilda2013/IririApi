using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class MemberRegistrationUser : IdentityUser
    {
        
       // public Guid MemberId { get; set; }

        [Column(TypeName = "nvarchar(130)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(130)")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        public string MemberAddress { get; set; }

        public string MemberEmail { get; set; }

        public string MemberPhone{ get; set; }

        public string  Occupation { get; set; }

        public DateTime DOB { get; set; }

        public bool Status { get; set; }

        public string CardNo { get; set; }

        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "nvarchar(130)")]
        public string CreatedBy { get; set; }

        public bool IsPasswordChangeRequired { get; set; }

        public bool IsPasswordChanging { get; set; }

        public UserRole Role { get; set; }



    }
}
