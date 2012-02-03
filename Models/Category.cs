using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WAP_Assignment.Models
{
    public class Category
    {
        public string type { get; set; }
        
        public string name { get; set; }
        
        public string url { get; set; }
        
        [Key]
        public int id { get; set; }
    }
}