using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    public class Image
    {
        public string Type { get; set; }
        
        [DataType(DataType.ImageUrl)]
        public string Url { get; set; }
        
        public string Size { get; set; }
        
        public string ImageID { get; set; }
    }
}