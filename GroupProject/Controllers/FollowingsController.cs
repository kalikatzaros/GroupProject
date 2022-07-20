using GroupProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class FollowingsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: ApplicationUser
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetPeopleIFollow()
        {
            string userId = User.Identity.GetUserId();
            var followees = _context.Followings
                                .Where(f => f.FollowerId == userId)
                                .Select(f => f.Followee)
                                .ToList();

            return View(followees);

        }

        public ActionResult GetPeopleThatFollowMe()
        {
            var userId = User.Identity.GetUserId();
            var followers = _context.Followings
                .Where(f => f.FolloweeId == userId)
                .Select(f => f.Follower)
                .ToList();
            return View(followers);
        }



    }
}