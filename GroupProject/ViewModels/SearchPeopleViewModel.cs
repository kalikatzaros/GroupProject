using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.ViewModels
{
    public class SearchPeopleViewModel
    {
        public ApplicationUser User { get; set; }
        public List<ApplicationUser> Followees { get; set; }
        public List<ApplicationUser> Followers { get; set; }
    }
}