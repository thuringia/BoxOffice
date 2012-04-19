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
        /// <summary>
        /// This rating's unique ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingID { get; set; }

        /// <summary>
        /// The MovieID FK of the rented movie
        /// </summary>
        public int MovieID { get; set; }

        /// <summary>
        /// The navigation property for the rented movie
        /// </summary>
        public virtual Movie Movie { get; set; }

        /// <summary>
        /// The renter's UserID as FK
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// The renter's User object as navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// The date the rental was issued
        /// </summary>
        public DateTime? DateRented { get; set; }
    }
}