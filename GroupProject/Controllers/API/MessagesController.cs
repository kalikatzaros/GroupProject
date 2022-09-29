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
    [Authorize]
    [RoutePrefix("api/messages")]
    public class MessagesController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();


        public MessagesController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: api/Messages


        // GET: api/Messages/5

        
        [Route("")]
        public IHttpActionResult GetIncomingMessages()
        {
            var id = User.Identity.GetUserId();

            var incomingMessages = _context.Messages
                            .Include("Sender")
                            .Include("Receiver")
                            .Where(m => m.ReceiverId == id)
                            .OrderByDescending(m => m.Datetime);

            return Ok(incomingMessages.ToList());
        }

        [HttpGet]
        [Route("getUnreadMessageCount")]
        public IHttpActionResult GetUnreadMessageCount()
        {
            var id = User.Identity.GetUserId();
            var count = _context.Messages
                .Where(m => m.ReceiverId == id && m.IsRead==false)
                .ToList().Count();

            return Ok(count);
        }
        [HttpPost]
        [Route("changeStatus/{id}")]
        public IHttpActionResult ChangeStatus(int? id)
        {
            var message = _context.Messages
                .SingleOrDefault(m => m.Id == id);
            if (message.IsRead == false)
            {
                message.IsRead = true;
               _context.Entry(message).State = EntityState.Modified;
               _context.SaveChanges();
            }
                                         
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult Delete(int? id)
        {
            var messageToBeDeleted = _context.Messages
                                        .SingleOrDefault(m => m.Id == id);
            _context.Messages.Remove(messageToBeDeleted);
            _context.SaveChanges();

            return Ok(messageToBeDeleted);
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