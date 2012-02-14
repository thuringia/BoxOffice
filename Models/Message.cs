using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WAP_Assignment.Models
{
    /// <summary>
    /// A Model class describing a message between users
    /// </summary>
    public class Message
    {
        public int MessageID { get; set; }

        public virtual ICollection<User> Users { get; set; }

        [Required]
        public string From { get; set; }

        public string To { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Text { get; set; }

        public bool toAll { get; set; }
    }
}