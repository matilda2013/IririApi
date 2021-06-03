using IririApi.Libs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{

    public class Announcement 
    {
        [Key]
        public Guid AnnoucdId { get; set; }
        public DateTime AnnouceDate { get; set; }
        public string Annoucement { get; set; }
    }

}
