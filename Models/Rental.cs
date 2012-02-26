using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoxOffice.Models
{
    public class Rental
    {
        public int RentalID { get; set; }

        public int DvdID { get; set; }
        public virtual ICollection<DVD> Dvd { get; set; }

        public int UserID { get; set; }
        public virtual ICollection<User> User { get; set; }

        public DateTime DateOfRental { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? DateReturned { get; set; }
    }
}