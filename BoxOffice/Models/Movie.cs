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

        /// <summary>
        /// The rentals and queue items associated with this movie
        /// </summary>
        public virtual ICollection<Rental> Rentals { get; set; }

        /// <summary>
        /// Marks this movie as the movie of the week
        /// </summary>
        public bool? MovieOfTheWeek { get; set; }

        /// <summary>
        /// The number of times this movie has been rented
        /// </summary>
        public int RentalCount
        {
            get
            {
                return this.Rentals.Count;
            }
        }

        /// <summary>
        /// The date this movie was added to BoxOffice
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? DateAdded { get; set; }

        #endregion

        #region Fields as used by themovidb.org

        /// <summary>
        /// Indicates whether a movie is considered adult material and has to be blocked from underage users
        /// </summary>
        public bool? Adult { get; set; }

        /// <summary>
        /// Shows that a movie has been translated, indicating international availability
        /// </summary>
        public bool? Translated { get; set; }

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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        
        /// <summary>
        /// the # of votes as delivered by TMDB
        /// </summary>
        public int Votes_by_moviedb { get; set; }
        
        /// <summary>
        /// this movie's rating as reported by TMDB
        /// </summary>
        public float? Rating_by_moviedb { get; set; }
        
        /// <summary>
        /// this movie's tagline
        /// </summary>
        public string Tagline { get; set; }
        
        /// <summary>
        /// this movies content rating
        /// </summary>
        public string Certification { get; set; }
        
        /// <summary>
        /// the date this movie's been released
        /// </summary>
        public DateTime? DateReleased { get; set; }
        
        /// <summary>
        /// this movies homepage
        /// </summary>
        public string Homepage { get; set; }
        
        /// <summary>
        /// this movies trailer
        /// </summary>
        public string Trailer { get; set; }

        /// <summary>
        /// this movie's categories
        /// </summary>
        public virtual ICollection<Category> Categories { get; set; }

        /// <summary>
        /// this movie's production studios
        /// </summary>
        public virtual ICollection<Studio> Studios { get; set; }

        /// <summary>
        /// this movie's production country
        /// </summary>
        public virtual ICollection<Country> Countries { get; set; }

        /// <summary>
        /// this movie's images
        /// </summary>
        public virtual ICollection<Image> Images { get; set; }

        /// <summary>
        /// this movie's cast members
        /// </summary>
        public virtual ICollection<CastMember> Cast { get; set; }
        #endregion

        /// <summary>
        /// returns a compact string representation of this movie
        /// </summary>
        /// <returns>NAME with DVDs.Count DVDs for PRICE </returns>
        public override string ToString()
        {
            return this.Name + " with " + this.DVDs.Count() + " DVDs for " + this.Price;
        }
    }
}