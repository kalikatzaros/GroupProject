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
    [RoutePrefix("api/topicPosts")]
    public class TopicPostsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public TopicPostsController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("")]
        public IHttpActionResult GetTopicPosts()
        {
            var topicPosts = _context.TopicPosts
                             .Include(tp=>tp.Post)
                             .Include(tp=>tp.Sender)
                             .Include(tp=>tp.Topic)
                             .ToList();

            return Ok(topicPosts);
        }

        [HttpDelete]
        //[Route("deletePost")]
        public IHttpActionResult DeleteTopicPost(int? id)
        {
            var topicPostToBeDeleted = _context.TopicPosts
                                        .SingleOrDefault(tp => tp.Id == id);
            _context.TopicPosts.Remove(topicPostToBeDeleted);
            _context.SaveChanges();

            return Ok(topicPostToBeDeleted);
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
