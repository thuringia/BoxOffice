using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BoxOffice.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public int Flag { get; set; }
    }
}