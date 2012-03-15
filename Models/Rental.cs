﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoxOffice.Models
{
    public class Rental
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalID { get; set; }

        public int DvdID { get; set; }
        public virtual DVD Dvd { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        public DateTime DateOfRental { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? DateReturned { get; set; }
    }
}