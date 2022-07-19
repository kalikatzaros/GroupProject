using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public ICollection<TopicPost> TopicPosts { get; set; }
    }
}