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
        /// <summary>
        /// auto inceremented ID for the comment
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }

        /// <summary>
        /// MovieID as FK
        /// </summary>
        public int MovieID { get; set; }

        /// <summary>
        /// Movie as navigation property for easy access to the data
        /// </summary>
        public virtual Movie Movie { get; set; }

        /// <summary>
        /// UserID as FK
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// User as navigation property for easy access
        /// </summary>
        public virtual User User { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public int? Flag { get; set; }
    }
}