using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    public class CastMember
    {
        /// <summary>
        /// This cast member's unique ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CastMemberID { get; set; }

        /// <summary>
        /// This cast member's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The character this cast member played
        /// </summary>
        public string Character { get; set; }
        
        /// <summary>
        /// This cast member's job during the movie's production
        /// </summary>
        public string Job { get; set; }

        // public ??? thumb { get; set; }
        
        /// <summary>
        /// The department the cast member worked in
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// TMDb link with further info
        /// </summary>
        [DataType(DataType.Url)]
        public string Url { get; set; }
        
        /// <summary>
        /// the position of this cast member (supplied by TMDb)
        /// </summary>
        public int Order { get; set; }
        
        /// <summary>
        /// This cast member's ID inside the cast
        /// </summary>
        public int Cast_id { get; set; }

        /// <summary>
        /// The Movie this cast appeared in (FK)
        /// </summary>
        public int MovieID { get; set; }

        /// <summary>
        /// The Movie this cast appeared in (navigation property)
        /// </summary>
        public virtual Movie Movie { get; set; }
    }
}