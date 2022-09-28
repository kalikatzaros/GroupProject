using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Owin;

namespace GroupProject.Repositories
{
    public class MessageRepository : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Message> GetIncomingMessages()
        {
            var id = HttpContext.Current.User.Identity.GetUserId();
            var messages = _context.Messages
                            .Include("SenderId")
                            .Include("ReceiverId")
                            .Where(m => m.ReceiverId == id);
            return messages;

        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}