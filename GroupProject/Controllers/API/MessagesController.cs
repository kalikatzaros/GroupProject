using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using GroupProject.Models;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;

namespace GroupProject.Controllers.API
{
    public class MessagesController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();       
        

        public MessagesController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: api/Messages
        

        // GET: api/Messages/5
        
        //[Authorize]
        public IHttpActionResult GetIncomingMessages()
        {
            var id = User.Identity.GetUserId();

            

            var incomingMessages = _context.Messages
                            //.Include("Sender")
                            //.Include("Receiver")
                            .Where(m => m.ReceiverId == id)
                            .OrderByDescending(m => m.Datetime);
            return Ok(incomingMessages.ToList());
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}