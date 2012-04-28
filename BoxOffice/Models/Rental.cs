using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    public class Rental
    {
        /// <summary>
        /// The unique ID for a rental
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalID { get; set; }

        /// <summary>
        /// The rental's associated DvdID FK
        /// </summary>
        public int? DvdID { get; set; }

        /// <summary>
        /// The rental's DVD as navigation property
        /// </summary>
        public virtual DVD Dvd { get; set; }

        /// <summary>
        /// The renter's UserID as FK
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// The renter as navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// The date the rental was issued
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? DateOfRental { get; set; }

        /// <summary>
        /// The date the DVD was sent to the renter
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? DateSent { get; set; }

        /// <summary>
        /// The date the DVD is due to be returned
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? DateDue { get; set; }

        /// <summary>
        /// The date the DVD actually returned
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? DateReturned { get; set; }

        /// <summary>
        /// the movie associated with this rental
        /// </summary>
        public virtual Movie Movie { get; set; }

        /// <summary>
        /// the associated movie's MovieID FK
        /// </summary>
        public int MovieID { get; set; }

        /// <summary>
        /// The position of this Rental in the user's queue,
        /// null = not in queue, i.e. rental returned
        /// </summary>
        public int? QueuePosition { get; set; }
    }
}