﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    public class Studio
    {
        public string Name { get; set; }
        
        public string Url { get; set; }
        
        public int StudioID { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}