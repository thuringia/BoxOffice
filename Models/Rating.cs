using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WAP_Assignment.Models
{
    public class Rating
    {
        public int RatingID { get; set; }

        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        public DateTime Date { get; set; }
    }
}