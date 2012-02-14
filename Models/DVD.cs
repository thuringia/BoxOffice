using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAP_Assignment.Models
{
    /// <summary>
    /// A Model class describing a physical DVD, that can be sent to a customer
    /// </summary>
    public class DVD
    {
        public int DvdID { get; set; }
        public int MovieID { get; set; }
        public IEnumerable<Rental> Rentals { get; set; }
        public string State { get; set; }
    }
}