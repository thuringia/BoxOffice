using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BoxOffice.Models
{
    public class Rating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingID { get; set; }

        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }


        public DateTime DateRented { get; set; }
    }
}