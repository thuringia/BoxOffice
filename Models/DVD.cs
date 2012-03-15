using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    /// <summary>
    /// A Model class describing a physical DVD, that can be sent to a customer
    /// </summary>
    public class DVD
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DvdID { get; set; }

        public int MovieID { get; set; }
        public Movie Movie { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
        
        public string State { get; set; }
    }
}