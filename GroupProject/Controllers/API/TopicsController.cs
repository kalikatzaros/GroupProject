using GroupProject.Dtos;
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
        [Route("addTopic")]
        [HttpPost]
        public IHttpActionResult AddTopic(TopicDto dto)
        {
            var userId = User.Identity.GetUserId();


            var topic = new Topic()
            {
                Title = dto.Title,
                Created = DateTime.Now,
                UserId = userId
            };

            if (ModelState.IsValid)
            {
              _context.Topics.Add(topic);
                _context.SaveChanges();
                var post = new Post()
                {
                    Body = dto.Body,
                    Datetime = DateTime.Now
                };
               _context.Posts.Add(post);
               _context.SaveChanges();
                var topicPost = new TopicPost()
                {
                    PostId = post.Id,
                    SenderId = userId,
                    TopicId = topic.Id
                };
               _context.TopicPosts.Add(topicPost);
                _context.SaveChanges();
                return Ok();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("deleteTopic/{id}")]
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
