﻿using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.ViewModels
{
    public class GetTopicPostsViewModel
    {
        public ApplicationUser LoggedInUser { get; set; }

        public List<TopicPost> TopicPosts { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public GetTopicPostsViewModel()
        {
            TopicPosts = new List<TopicPost>();
        }

    }
}