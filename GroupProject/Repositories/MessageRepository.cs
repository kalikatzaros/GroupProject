using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Owin;

namespace GroupProject.Repositories
{
    public class MessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<Message> GetIncomingMessages()
        {
            var id = HttpContext.Current.User.Identity.GetUserId();
            var messages = _context.Messages
                            .Include("Sender")
                            .Include("Receiver")
                            .Where(m => m.ReceiverId == id);
            return messages;

        }
    }
}