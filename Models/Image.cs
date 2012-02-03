using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WAP_Assignment.Models
{
    public class Image
    {
        public string type { get; set; }
        
        public string url { get; set; }
        
        public string size { get; set; }
        
        [Key]
        public string id { get; set; }
    }
}