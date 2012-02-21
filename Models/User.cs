using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WAP_Assignment.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// A user's password, has to be at least 6 characters long
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        public int AdressID { get; set; }
        [Required]
        public virtual Adress Adress { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        public bool isAdmin { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Movie> Queue { get; set; }
    }
}