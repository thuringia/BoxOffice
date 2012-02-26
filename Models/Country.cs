using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    public class Country
    {
        public string Name { get; set; }
        
        [Key]
        public string Code { get; set; }
        
        public string Url { get; set; }
    }
}