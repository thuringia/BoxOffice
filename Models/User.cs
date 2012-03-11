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
        public int UserID { get; set; }

        [MaxLength(30)]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        //public int AdressID { get; set; }
        //public virtual Adress Adress { get; set; }
        public string Street { get; set; }

        [MaxLength(7)]
        public string Number { get; set; }

        [MaxLength(8)]
        public string Zip { get; set; }

        public string City { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Rental> Queue { get; set; }
    }
}