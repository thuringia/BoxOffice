using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    /// <summary>
    /// A Model class describing a message between users
    /// </summary>
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageID { get; set; }

        public virtual ICollection<User> Users { get; set; }

        [Required]
        public int FromUserID { get; set; }

        public int? ToUserID { get; set; }

        [Required]
        public DateTime DateSent { get; set; }

        [Required]
        public string Text { get; set; }

        public bool toAll { get; set; }
    }
}