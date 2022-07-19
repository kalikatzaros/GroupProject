using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class WallPost
    {
        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }
        public User User { get; set; }
        [Key]
        [Column(Order = 2)]
        public int PostId { get; set; }
        public Post Post { get; set; }

    }
}