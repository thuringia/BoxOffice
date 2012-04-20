using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    public class Country
    {
        /// <summary>
        /// The name of this country
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The code by which this country is identified
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Code { get; set; }
        
        /// <summary>
        /// TMDb link with further info
        /// </summary>
        [DataType(DataType.Url)]
        public string Url { get; set; }

        /// <summary>
        /// The movies produced by this country
        /// </summary>
        public virtual ICollection<Movie> Movies { get; set; }
    }
}