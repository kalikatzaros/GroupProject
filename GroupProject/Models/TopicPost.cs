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
        [Key]
        [Column(Order =1)]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        [Key]
        [Column(Order = 2)]
        public int PostId { get; set; }
        public Post Post { get; set; }

        //public ICollection<TopicPost> Topics { get; set; }
        //public ICollection<TopicPost> Posts { get; set; }


        //public TopicPost()
        //{
        //    Topics = new Collection<TopicPost>();
        //    Posts = new Collection<TopicPost>();
        //}
    }
}
