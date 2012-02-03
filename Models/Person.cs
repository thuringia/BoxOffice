using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WAP_Assignment.Models
{
    public class Person
    {
        public string name { get; set; }
        
        public string character { get; set; }
        
        public string job { get; set; }
        
        [Key]
        public int id { get; set; }
        
        // public ??? thumb { get; set; }
        
        public string department { get; set; }
        
        public string url { get; set; }
        
        public string order { get; set; }
        
        public string cast_id { get; set; }
    }
}