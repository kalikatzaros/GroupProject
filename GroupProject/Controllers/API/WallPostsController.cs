using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GroupProject.Controllers.API
{
    [Authorize]
    [RoutePrefix("api/wallposts")]
    public class WallPostsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public WallPostsController()
        {
            _context = new ApplicationDbContext();
        }
        [Route("")]
        public IHttpActionResult GetWallPosts()
        {
            var wallposts = _context.WallPosts
                             .Include(wp => wp.Post)
                             .Include(wp => wp.User)
                             .ToList();

            return Ok(wallposts);
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
            var wallPostToBeDeleted = _context.WallPosts
                                              .SingleOrDefault(wp => wp.Id == id);

            _context.WallPosts.Remove(wallPostToBeDeleted);
            _context.SaveChanges();

            return Ok(wallPostToBeDeleted);
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
