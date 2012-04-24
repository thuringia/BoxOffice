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

        /// <summary>
        /// Should this movie be promoted as the new movie of the week?
        /// </summary>
        [Required]
        public bool MovieOfTheWeek { get; set; }

        /// <summary>
        /// The movie's ID on themoviedb.org, needed to fetch the metadata
        /// </summary>
        [Required]
        public string TMDbID { get; set; }
    }
}