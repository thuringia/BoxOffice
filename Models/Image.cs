using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    public class Image
    {
        /// <summary>
        /// The type of this image, may be "poster" or "backdrop"
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// The URL to this image
        /// </summary>
        [DataType(DataType.ImageUrl)]
        public string Url { get; set; }
        
        /// <summary>
        /// the size of the image
        /// </summary>
        public string Size { get; set; }
        
        /// <summary>
        /// The image's unique ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ImageID { get; set; }
    }
}