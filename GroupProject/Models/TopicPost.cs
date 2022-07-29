using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class TopicPost
    {
        public int Id { get; set; }
        [Required]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        [Required]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        [Required]
        public int PostId { get; set; }
        public Post Post { get; set; }

    }
}
