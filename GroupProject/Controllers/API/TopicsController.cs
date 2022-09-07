using GroupProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace GroupProject.Controllers.API
{
    [Authorize]
    [RoutePrefix("api/topics")]
    public class TopicsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public TopicsController()
        {
            _context = new ApplicationDbContext();
        }
        [Route("")]
        public IHttpActionResult GetTopics()
        {
            var topics = _context.Topics
                             .Include(t => t.User)
                             .ToList();
      
            return Ok(topics);
        }

        [HttpGet]
        [Route("getUnreadMessageCount")]
        public IHttpActionResult GetUnreadMessageCount()
        {
            var id = User.Identity.GetUserId();
            var count = _context.Messages
                .Where(m => m.ReceiverId == id && m.IsRead == false)
                .ToList().Count();

            return Ok(count);
        }
        [HttpPost]
        [Route("changeStatus")]
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
        public IHttpActionResult DeleteTopic(int? id)
        {
            var topicToBeDeleted = _context.Topics
                                        .SingleOrDefault(t => t.Id == id);
            _context.Topics.Remove(topicToBeDeleted);
            _context.SaveChanges();

            return Ok(topicToBeDeleted);
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
