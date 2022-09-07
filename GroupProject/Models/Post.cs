using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Post
    {
        public int Id { get; set; }
   
        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Datetime { get; set; }

        [NotMapped]
        public string ShorterBody
        {
            get
            {
                return GetShorterBody();

            }
        }
        public ICollection<TopicPost> TopicPosts { get; set; }
        public ICollection<WallPost> WallPosts { get; set; }

        private string GetShorterBody()
        {
            if (Body.Length > 9)
            {
                var shortBody = Body.Substring(0, 10);

                return String.Concat(shortBody, "...");
            }
            return String.Concat(Body, "...");
        }
    }
}