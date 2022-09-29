using GroupProject.Dtos;
using GroupProject.Models;
using GroupProject.Repositories;
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
        private readonly TopicRepository _topicRepository;
        private readonly TopicPostRepository _topicPostRepository;
        private readonly PostRepository _postRepository;
        public TopicsController()
        {
            _context = new ApplicationDbContext();
            _topicRepository = new TopicRepository(_context);
            _topicPostRepository = new TopicPostRepository(_context);
            _postRepository = new PostRepository(_context);
        }
        [Route("")]
        public IHttpActionResult GetTopics()
        {
            var topics = _topicRepository.GetAll();
            //var topics = _context.Topics
            //                 .Include(t => t.User)
            //                 .ToList();
      
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
                //_context.Topics.Add(topic);
                //  _context.SaveChanges();
                _topicRepository.Create(topic);

                var post = new Post()
                {
                    Body = dto.Body,
                    Datetime = DateTime.Now
                };
               _postRepository.Create(post);
             
                var topicPost = new TopicPost()
                {
                    PostId = post.Id,
                    SenderId = userId,
                    TopicId = topic.Id
                };

              _topicPostRepository.Create(topicPost);
                
                return Ok();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("deleteTopic/{id}")]
        public IHttpActionResult DeleteTopic(int? id)
        {
           
            _topicRepository.Delete(id);
            

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _postRepository.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
