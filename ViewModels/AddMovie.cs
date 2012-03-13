using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace BoxOffice.Models
{
    public class AddMovieModel
    {
        /// <summary>
        /// A name for the movie that is to be added
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The # of DVDs for this movie
        /// </summary>
        [Required]
        public int DVDs { get; set; }

        /// <summary>
        /// The price for one rental
        /// </summary>
        [Required]
        public decimal Price { get; set; }
    }
}