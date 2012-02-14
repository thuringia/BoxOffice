using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WAP_Assignment.Models
{
    public class Person
    {
        public string Name { get; set; }
        
        public string Character { get; set; }
        
        public string Job { get; set; }
        
        public int PersonID { get; set; }
        
        // public ??? thumb { get; set; }
        
        public string Department { get; set; }
        
        public string Url { get; set; }
        
        public string Order { get; set; }
        
        public string Cast_id { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}