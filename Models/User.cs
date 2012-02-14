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
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; }

        public int AdressID { get; set; }
        [Required]
        public virtual Adress Adress { get; set; }

        public int Phone { get; set; }

        public bool isAdmin { get; set; }
    }
}