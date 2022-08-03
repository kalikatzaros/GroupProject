using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.ViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ProfileImage { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<WallPost> WallPosts { get; set; }
        public Post Post { get; set; }
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Datetime { get; set; }



    }
}