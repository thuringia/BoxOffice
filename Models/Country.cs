using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WAP_Assignment.Models
{
    public class Country
    {
        public string name { get; set; }
        
        [Key]
        public string code { get; set; }
        
        public string url { get; set; }
    }
}