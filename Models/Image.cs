using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WAP_Assignment.Models
{
    public class Image
    {
        public string Type { get; set; }
        
        public string Url { get; set; }
        
        public string Size { get; set; }
        
        public string ImageID { get; set; }
    }
}