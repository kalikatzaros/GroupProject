using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.Dtos
{
    public class TopicDto
    {
        public int TopicId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public Topic Topic { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        public Post Post { get; set; }
        public ApplicationUser LoggedInUser { get; set; }
    }
}