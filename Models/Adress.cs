using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WAP_Assignment.Models
{
    public class Adress
    {
        /// <summary>
        /// Describe the relationship between User <-> Adress => 1:1
        /// </summary>
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int AdressID { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        [MaxLength(7)]
        public string Number { get; set; }

        [Required]
        [MaxLength(8)]
        public string Zip { get; set; }

        [Required]
        public string City { get; set; }
    }
}