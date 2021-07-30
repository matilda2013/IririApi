using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.ViewModel
{
    public class GalleryViewModel
    {
        public string FileName { get; set; }

        public string FileTitle { get; set; }

       public List<IFormFile> FileImage { get; set; }

        public IList<string> base64 { get; set; }

        public string Description { get; set; }

        public string Event { get; set; }
    }
}
