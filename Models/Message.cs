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
        /// <summary>
        /// The unique ID of this message
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageID { get; set; }

        /// <summary>
        /// The Users participating in this message
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// The sending user's UserID
        /// </summary>
        [Required]
        public int FromUserID { get; set; }

        /// <summary>
        /// The receiving user's UserID, null if sent to all
        /// </summary>
        public int? ToUserID { get; set; }

        /// <summary>
        /// The date the message was sent
        /// </summary>
        [Required]
        public DateTime DateSent { get; set; }

        /// <summary>
        /// The message's text
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Indicates whether the message is supposed to be sent to all members
        /// </summary>
        [Required]
        public bool toAll { get; set; }
    }
}