using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WAP_Assignment.Models
{
    [Bind (Exclude = "id") ]
    public class Movie
    {
        #region Fields required by MovieBox

        /// <summary>
        /// The amount of discs available of that movie
        /// </summary>
        [Required]
        public int amount { get; set; }
        
        /// <summary>
        /// The price of one rental
        /// </summary>
        [Required]
        public decimal price { get; set; }

        /// <summary>
        /// The number of votes by users of MovieBox
        /// </summary>
        [ScaffoldColumn(false)]
        public int votes { get; set; }

        /// <summary>
        /// The rating of the movie by users of MovieBox
        /// </summary>
        [ScaffoldColumn(false)]
        public float rating { get; set; }

        #endregion

        #region Fields as used by themovidb.org

        /// <summary>
        /// Indicates whether a movie is considered adult material and has to be blocked from underage users
        /// </summary>
        public bool adult { get; set; }

        /// <summary>
        /// Shows that a movie has been translated, indicating international availability
        /// </summary>
        [ScaffoldColumn(false)]
        public bool translated { get; set; }

        /// <summary>
        /// A movie's original language
        /// </summary>
        [Required]
        public string language { get; set; }
        
        /// <summary>
        /// A movie's original name
        /// </summary>
        public string original_name { get; set; }
        
        /// <summary>
        /// A movie's name, may change because of translation
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// An alternative name for the movie
        /// </summary>
        public string alternative_name { get; set; }
        
        /// <summary>
        /// The media type, in this case it's always movie
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// The movie's unique ID on themoviedb.org
        /// </summary>
        [Key]
        [ScaffoldColumn(false)]
        public int id { get; set; }

        /// <summary>
        /// The movie's ID on imdb.com
        /// </summary>
        [ScaffoldColumn(false)]
        public string imdb_id { get; set; }
        
        /// <summary>
        /// URL referring to the movie's info page on themoviedb.org
        /// </summary>
        public string url { get; set; }
        
        /// <summary>
        /// A synopsis of the movie's contents
        /// </summary>
        public string overview { get; set; }
        
        [ScaffoldColumn(false)]
        public int votes_by_moviedb { get; set; }
        
        [ScaffoldColumn(false)]
        public float rating_by_moviedb { get; set; }
        
        public string tagline { get; set; }
        
        public string certification { get; set; }
        
        public DateTime released { get; set; }
        
        public string homepage { get; set; }
        
        public string trailer { get; set; }
        
        public List<Category> categories { get; set; }
        
        public List<Studio> studios { get; set; }
        
        public List<Country> countries { get; set; }
        
        public List<Image> images { get; set; }
        
        public List<Person> cast { get; set; }
        #endregion
    }
}