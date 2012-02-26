using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    public class Category
    {
        public string Type { get; set; }
       
        public string Name { get; set; }
        
        public string Url { get; set; }
        
        public int CategoryID { get; set; }

        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }
    }
}