using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BoxOffice.Models
{
    public class Adress
    {
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