using GroupProject.Models;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class FollowersController : Controller
    {
        private readonly ApplicationDbContext _context;


        public FollowersController()
        {
            _context = new ApplicationDbContext();
        }
              
        public ActionResult GetPeopleThatFollowMe(string search)
        {
            string userId = User.Identity.GetUserId();
            
            //var roleId = _context.Roles.Where(r => r.Name == "Admin").Select(r => r.Id).SingleOrDefault();


            ViewBag.LoggedUser = _context.Users.SingleOrDefault(u => u.Id == userId);
            var viewModel = new SearchPeopleViewModel();
            var followees = _context.Followings
                          .Where(f => f.FollowerId == userId && f.Followee.IsDeactivated == false)
                          .Select(f => f.Followee)
                          .ToList();
            if (search == null)
            {
                var followers = _context.Followings
                            .Where(f => f.FolloweeId == userId && f.Follower.IsDeactivated == false)
                            .Select(f => f.Follower)
                            .ToList();

                viewModel.Followers = followers;
                viewModel.Followees = followees;
                return View(viewModel);
            }
            else
            {
                var followers = _context.Followings
                                  .Where(f => f.FolloweeId == userId && f.Follower.IsDeactivated == false && (f.Followee.Name.ToLower() == search.ToLower()
                         || f.Follower.LastName.ToLower() == search.ToLower()
                         || f.Follower.Email.ToLower() == search.ToLower()))
                                  .Select(f => f.Follower)
                               .ToList();

                viewModel.Followers = followers;
                viewModel.Followees = followees;
                return View(viewModel);
            }
           
        }

        public ActionResult GetFollowers(string search)
        {
            string userId = User.Identity.GetUserId();

            //var roleId = _context.Roles.Where(r => r.Name == "Admin").Select(r => r.Id).SingleOrDefault();


            ViewBag.LoggedUser = _context.Users.SingleOrDefault(u => u.Id == userId);
            var viewModel = new SearchPeopleViewModel();
            var followees = _context.Followings
                          .Where(f => f.FollowerId == userId && f.Followee.IsDeactivated == false)
                          .Select(f => f.Followee)
                          .ToList();
            if (search == null)
            {
                var followers = _context.Followings
                            .Where(f => f.FolloweeId == userId && f.Follower.IsDeactivated == false)
                            .Select(f => f.Follower)
                            .ToList();

                viewModel.Followers = followers;
                viewModel.Followees = followees;
                return View(viewModel);
            }
            else
            {
                var followers = _context.Followings
                                  .Where(f => f.FolloweeId == userId && f.Follower.IsDeactivated == false && (f.Followee.Name.ToLower() == search.ToLower()
                         || f.Follower.LastName.ToLower() == search.ToLower()
                         || f.Follower.Email.ToLower() == search.ToLower()))
                                  .Select(f => f.Follower)
                               .ToList();

                viewModel.Followers = followers;
                viewModel.Followees = followees;
                return View(viewModel);
            }

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