using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.IO;
using GroupProject.Repositories;

namespace GroupProject.Controllers.API
{
    [Authorize]
    [RoutePrefix("api/wallposts")]
    public class WallPostsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly WallPostRepository _wallPostRepository;
        public WallPostsController()
        {
            _context = new ApplicationDbContext();
            _wallPostRepository = new WallPostRepository(_context);
        }
       

        [HttpGet]
        [Route("viewWallPost/{id}")]
        public IHttpActionResult ViewWallPost(int? id)
        {
            var postBody = _context.WallPosts
                                   .SingleOrDefault(wp => wp.Id == id).Post.Body;

            return Ok(postBody);
        }

        [HttpDelete]
        [Route("deleteWallPost/{id}")]
        public IHttpActionResult DeleteWallPost(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
           
            _wallPostRepository.Delete(id);

            return Ok();
        }

        [HttpPost]
        [Route("addWallPost")]
        public IHttpActionResult AddWallPost(string imageFile,string body)
        {
            var userId = User.Identity.GetUserId();
            var post = new Post();
            post.Body = body;
            post.Datetime = DateTime.Now;

            _context.Posts.Add(post);
            _context.SaveChanges();

            var wallPost = new WallPost()
            {
                UserId = userId,
                PostId = post.Id
            };
            _wallPostRepository.Create(wallPost);
           

            return Ok(wallPost);
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
