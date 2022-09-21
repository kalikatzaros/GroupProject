using GroupProject.Dtos;
using GroupProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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

        [Route("getTopicPosts/{id}")]
        [HttpGet]
        public IHttpActionResult GetTopicPosts(int? id)
        {
            var topic = _context.Topics.SingleOrDefault(t => t.Id == id);
            if (topic == null)
            {
                return NotFound();
            }
            var userId = User.Identity.GetUserId();
            var loggedUser = _context.Users.SingleOrDefault(u => u.Id == userId);
            var topicPosts = _context.TopicPosts
                             .Include(tp=>tp.Post)
                             .Include(tp=>tp.Sender)
                             .Include(tp=>tp.Topic)
                             .Where(tp=>tp.Topic.Id==id)
                             .ToList();
            var dto = new TopicPostsDto()
            {
                TopicPosts=topicPosts,
                LoggedUser=loggedUser
            };
            return Ok(dto);
        }

        [HttpGet]
        [Route("viewPost/{id}")]
        public IHttpActionResult ViewPost(int? id)
        {
            var postBody = _context.TopicPosts
                .Include(tp => tp.Post)
                .SingleOrDefault(tp => tp.Id == id).Post.Body;
         
            return Ok(postBody);
        }

        [HttpGet]
        [Route("getAllLastTopicPosts")]
        public IHttpActionResult GetAllLastTopicPost()
        {
            var lastTopicPosts = new List<TopicPost>();
            foreach (var topic in _context.Topics.ToList())
            {
                var lastTopicPost = _context.TopicPosts
                .Include(t => t.Sender)
                .Include(t=>t.Topic.User)
                .Include(t => t.Post)
                .Include(t=>t.Topic)
                .Where(t => t.TopicId == topic.Id)
                .ToList().LastOrDefault();
                lastTopicPosts.Add(lastTopicPost);
            }
            

            return Ok(lastTopicPosts);
        }
        [HttpGet]
        [Route("getLastTopicPost/{id}")]
        public IHttpActionResult GetLastTopicPost(int? id)
        {
            var lastTopicPost = _context.TopicPosts
                .Include(t=>t.Sender)
                .Include(t=>t.Post)
                .Where(t => t.TopicId == id).ToList().LastOrDefault();

            return Ok(lastTopicPost);
        }

        [HttpPost]
        [Route("createTopicPost")]
        public IHttpActionResult CreateTopicPost(CreateTopicPostDto dto)
        {
            //string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            var userId = User.Identity.GetUserId();
            var post = new Post()
            {
                Body = dto.Body,
                Datetime = DateTime.Now

            };

            if (dto.ImageFile != null)
            {
                post.Thumbnail = Path.GetFileName(dto.ImageFile.FileName);
                string fullPath = Path.Combine(HttpContext.Current.Server.MapPath("~/img"), post.Thumbnail);
                dto.ImageFile.SaveAs(fullPath);
            }


            _context.Posts.Add(post);
            _context.SaveChanges();

            var topicPost = new TopicPost()
            {
                TopicId=dto.TopicId,
                PostId=post.Id,
                SenderId=userId

            };

            _context.TopicPosts.Add(topicPost);
            _context.SaveChanges();


            return Ok();
        }

        [HttpDelete]
        [Route("deletePost/{id}")]
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
