using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BoxOffice.Models
{
    [Bind (Exclude = "id") ]
    public class Movie
    {
        #region Fields required by BoxOffice
        
        /// <summary>
        /// The price of one rental
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// The number of votes by users of BoxOffice
        /// </summary>
        public int? Votes { get; set; }

        /// <summary>
        /// Navigation property for the ratings placed by the users 
        /// </summary>
        public virtual ICollection<Rating> Ratings { get; set; }

        /// <summary>
        /// The rating of the movie by users of BoxOffice
        /// </summary>
        public float? Rating { get; set; }

        /// <summary>
        /// The Comments for that movie
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// The physical DVDs of this movie.
        /// </summary>
        public virtual ICollection<DVD> DVDs { get; set; }

        #endregion

        #region Fields as used by themovidb.org

        /// <summary>
        /// Indicates whether a movie is considered adult material and has to be blocked from underage users
        /// </summary>
        public bool Adult { get; set; }

        /// <summary>
        /// Shows that a movie has been translated, indicating international availability
        /// </summary>
        public bool Translated { get; set; }

        /// <summary>
        /// A movie's original language
        /// </summary>
        public string Language { get; set; }
        
        /// <summary>
        /// A movie's original name
        /// </summary>
        public string Original_name { get; set; }
        
        /// <summary>
        /// A movie's name, may change because of translation
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// An alternative name for the movie
        /// </summary>
        public string Alternative_name { get; set; }
        
        /// <summary>
        /// The media type, in this case it's always movie
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The movie's unique ID on themoviedb.org
        /// </summary>
        public int MovieID { get; set; }

        /// <summary>
        /// The movie's ID on imdb.com
        /// </summary>
        public string Imdb_id { get; set; }
        
        /// <summary>
        /// URL referring to the movie's info page on themoviedb.org
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// A synopsis of the movie's contents
        /// </summary>
        public string Overview { get; set; }
        
        
        public int Votes_by_moviedb { get; set; }
        
        
        public float Rating_by_moviedb { get; set; }
        
        public string Tagline { get; set; }
        
        public string Certification { get; set; }
        
        public DateTime Released { get; set; }
        
        public string Homepage { get; set; }
        
        public string Trailer { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Studio> Studios { get; set; }

        public virtual ICollection<Country> Countries { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<CastMember> Cast { get; set; }
        #endregion
    }
}