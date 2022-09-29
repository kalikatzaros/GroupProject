using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Created { get; set; }
        public ICollection<TopicPost> TopicPosts { get; set; }

        public Topic()
        {
                
        }
    }
}