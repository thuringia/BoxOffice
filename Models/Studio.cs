using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WAP_Assignment.Models
{
    public class Studio
    {
        public string Name { get; set; }
        
        public string Url { get; set; }
        
        public int StudioID { get; set; }
    }
}