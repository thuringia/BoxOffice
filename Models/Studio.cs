using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    public class Studio
    {
        /// <summary>
        /// The name of this Studio
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// TMDb link to further info
        /// </summary>
        public string Url { get; set; }
     
        /// <summary>
        /// The unique ID of this studio
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudioID { get; set; }

        /// <summary>
        /// The movies released by this studio
        /// </summary>
        public virtual ICollection<Movie> Movies { get; set; }
    }
}