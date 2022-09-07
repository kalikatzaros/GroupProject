using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Areas.Administrator.ViewModels
{
    public class TopicsViewModel
    {
        public List<Topic> Topics { get; set; }
        public List<TopicPost> TopicPosts{get;set;}
       
    }
}