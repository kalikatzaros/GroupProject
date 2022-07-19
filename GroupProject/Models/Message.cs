using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Message
    {
        [Key]
        [Column(Order=1)]
        public string SenderId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string ReceiverId { get; set; }
        [Required]
        public ApplicationUser Sender { get; set; }
        [Required]
        public ApplicationUser Receiver { get; set; }
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Datetime { get; set; }
    }
}