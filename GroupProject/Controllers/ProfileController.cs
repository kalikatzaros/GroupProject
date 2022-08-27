using GroupProject.Models;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GroupProject.Repositories;


namespace GroupProject.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext _context;
        private readonly FollowingRepository _followingRepository;

        public ProfileController()
        {
            _context = new ApplicationDbContext();
            _followingRepository = new FollowingRepository(_context);
        }
        // GET: Profile
        [Authorize]
        public ActionResult Index()
        {
            
                var userId = User.Identity.GetUserId();    

                var user = _context.Users.Include(u => u.WallPosts)
                    .SingleOrDefault(u => u.Id == userId);
                var wallPosts = _context.WallPosts
                    .Include(w => w.Post)
                    .Where(w => w.UserId == userId);
                var viewModel = new ProfileViewModel()
                {
                    User = user,
                    Email = user.Email,
                    ProfileImage = user.Thumbnail,
                    FirstName = user.Name,
                    LastName = user.LastName,
                    WallPosts = wallPosts.ToList(),
                    DateOfBirth = user.DateOfBirth,
                    Followings = _followingRepository.GetFollowings(userId).ToLookup(a => a.FolloweeId),
                    ShowActions = User.Identity.IsAuthenticated
                };


            if (User.Identity.IsAuthenticated)
            {
                
                viewModel.IsFollowing = _followingRepository.GetFollowing(userId, user.Id) != null;

            }
            return View("Index", viewModel);
        }

        public ActionResult VisitProfile(string Id)
        {            
                var otherUserId = Id;
                var user = _context.Users.Include(u => u.WallPosts)
                        .SingleOrDefault(u => u.Id == otherUserId);
                var wallPosts = _context.WallPosts
                    .Include(w => w.Post)
                    .Where(w => w.UserId == otherUserId);
                var viewModel = new ProfileViewModel()
                {
                    User = user,
                    Email = user.Email,
                    ProfileImage = user.Thumbnail,
                    FirstName = user.Name,
                    LastName = user.LastName,
                    WallPosts = wallPosts.ToList(),
                    DateOfBirth = user.DateOfBirth,
                    Followings = _followingRepository.GetFollowings(otherUserId).ToLookup(a => a.FolloweeId),
                    ShowActions = User.Identity.IsAuthenticated
                };
                return View(viewModel);           
        }

        
        

    }
}