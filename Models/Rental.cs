using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAP_Assignment.Models
{
    public class Rental
    {
        public int RentalID { get; set; }

        public int DvdID { get; set; }
        public virtual ICollection<DVD> Dvd { get; set; }
    }
}