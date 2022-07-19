﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public User Sender { get; set; }
        [Required]
        public User Receiver { get; set; }
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Datetime { get; set; }
    }
}