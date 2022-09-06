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
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;


        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

       [HttpGet]
        public IHttpActionResult MyFollowings()
        {
            var userId = User.Identity.GetUserId();
            var myFollowings = _context.Followings
                .Include(f=>f.Followee)
                .Where(f => f.FollowerId == userId)
                .ToList();
            
            return Ok(myFollowings);
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userId && f.FollowerId == dto.FolloweeId))
            {
                return BadRequest("Following already exists you son of a bitch!!");
            }

            var following = new Following()
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult UnFollow(string Id)
        {
            var userId = User.Identity.GetUserId();
            var following = _context.Followings
                            .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == Id);
            if (following == null)

                return NotFound();

            _context.Followings.Remove(following);
            _context.SaveChanges();
            return Ok(Id);
        }

    }
}
