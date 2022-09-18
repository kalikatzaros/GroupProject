using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.ViewModels
{
    public class NewsFeedViewModel
    {
        public List<WallPost> Wallposts { get; set; }
        public List<TopicPost> TopicPosts { get; set; }
        public List<ApplicationUser> People { get; set; }
    }
}