using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Dtos
{
    public class TopicPostsDto
    {
        public List<TopicPost> TopicPosts { get; set; }
        public Topic Topic { get; set; }
        public Post Post { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser LoggedUser { get; set; }
        public TopicPostsDto()
        {
            TopicPosts = new List<TopicPost>();
        }
    }
}