using GroupProject.Models;
using System;
using System.Collections.Generic;
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


    }
}