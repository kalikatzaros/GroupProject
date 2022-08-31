using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.ViewModels
{
    public class PeopleViewModel
    {
        public List<ApplicationUser> AllUsers { get; set; }
        public List<ApplicationUser> Followers { get; set; }
        public List<ApplicationUser> Followees { get; set; }
        public List<ApplicationUser> UnknownUsers { get; set; }

        public string UserId { get; set; }



    }
}