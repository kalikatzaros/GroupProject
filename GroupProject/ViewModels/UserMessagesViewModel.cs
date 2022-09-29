using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.ViewModels
{
    public class UserMessagesViewModel
    {
        
        public IEnumerable<ApplicationUser> Users { get; set; }
        public Message Message { get; set; }
    }
}