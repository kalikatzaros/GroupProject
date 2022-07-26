﻿using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.ViewModels
{
    public class WallPostViewModel
    {

        public int Id { get; set; }              
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Datetime { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public string Thumbnail { get; set; }
    }
}