using IririApi.Libs.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Model
{
    public class Gallery 
    {

        [Key]
        public Guid FileId { get; set; }
        public string FileName{ get; set; }

        public string FileTitle { get; set; }

        
        public string FilePicture { get; set; }
      

        public string Description { get; set; }

        public string Event { get; set; }
    }
}
