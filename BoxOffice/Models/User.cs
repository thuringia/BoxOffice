using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BoxOffice.Models
{
    public class User
    {
        /// <summary>
        /// This user's unique ID inside BoxOffice's DB
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        /// <summary>
        /// The user's username which will appear f.e. on comments
        /// </summary>
        [StringLength(30)]
        public string Username { get; set; }

        /// <summary>
        /// The user's e-mail address
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// The user's date of birth
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// The street the user lives in
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// the user's house number
        /// </summary>
        [StringLength(7)]
        public string Number { get; set; }

        /// <summary>
        /// the user's zip code
        /// </summary>
        [StringLength(8)]
        public string Zip { get; set; }

        /// <summary>
        /// the user's city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// the user's phone number
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        /// <summary>
        /// The Ratings submitted by this user
        /// </summary>
        public virtual ICollection<Rating> Ratings { get; set; }

        /// <summary>
        /// The Comments submitted by this user
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// The Messages received by this user
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }

        /// <summary>
        /// The user's rental queue
        /// </summary>
        public virtual ICollection<Rental> Queue { get; set; }
    }
}