using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    public class Category
    {
        /// <summary>
        /// This category's type, is always "genre"
        /// </summary>
        public string Type { get; set; }
       
        /// <summary>
        /// This category's name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// TMDb link to this category's page
        /// </summary>
        [DataType(DataType.Url)]
        public string Url { get; set; }
        
        /// <summary>
        /// This category's unique ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryID { get; set; }

        /// <summary>
        /// The movies of this category
        /// </summary>
        public virtual ICollection<Movie> Movies { get; set; }
    }
}