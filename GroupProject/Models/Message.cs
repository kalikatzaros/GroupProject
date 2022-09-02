using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace GroupProject.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [NotMapped]
        public string ShorterBody
        {
            get 
            {
               return GetShorterBody();

            }
        }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Datetime { get; set; }

        public bool IsRead { get; set; } = false;

        private string GetShorterBody()
        {
            if (Body.Length > 9)
            {
                var shortBody = Body.Substring(0, 10);
                
                return String.Concat(shortBody, "...");
            }                        
            return String.Concat(Body, "...");
        }
    }
}