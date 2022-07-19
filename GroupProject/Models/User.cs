using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="First name is required")]
        [StringLength(70,MinimumLength =2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(70, MinimumLength = 2)]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get=>$"{FirstName} {LastName}";
        }
    }
}