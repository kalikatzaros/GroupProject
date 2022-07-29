using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.ViewModel
{
    public class UserPostsViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public Post Post { get; set; }
    }
}