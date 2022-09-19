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
                ViewBag.loggedUser = userId;

                var user = _context.Users.Include(u => u.WallPosts)
                    .SingleOrDefault(u => u.Id == userId);

                var wallPosts = _context.WallPosts
                    .Include(w => w.Post)
                    .Where(w => w.UserId == userId)
                    .OrderByDescending(w => w.Post.Datetime);

            var viewModel = new ProfileViewModel()
            {
                User = user,
                Email = user.Email,
                ProfileImage = user.Thumbnail,
                FirstName = user.Name,
                LastName = user.LastName,
                WallPosts = wallPosts.ToList(),
                DateOfBirth = user.DateOfBirth,
                //Followings = _followingRepository.GetFollowings(userId).ToLookup(a => a.FolloweeId),
                //ShowActions = User.Identity.IsAuthenticated,
                Description = user.Description,
                FolloweesCount = _followingRepository.GetFolloweesCount(userId),
                FollowersCount= _followingRepository.GetFollowersCount(userId)
            };


            //if (User.Identity.IsAuthenticated)
            //{
                
            //    viewModel.IsFollowing = _followingRepository.GetFollowing(userId, user.Id) != null;

            //}
            return View("Index", viewModel);
        }

        public ActionResult MyProfile()
        {
         
                var userId = User.Identity.GetUserId();
                ViewBag.loggedUser = userId;
            ViewBag.LoggedUser = _context.Users.SingleOrDefault(u => u.Id == userId);
            var user = _context.Users.Include(u => u.WallPosts)
                    .SingleOrDefault(u => u.Id == userId);

                var wallPosts = _context.WallPosts
                    .Include(w => w.Post)
                    .Where(w => w.UserId == userId)
                    .OrderByDescending(w => w.Post.Datetime);

                var viewModel = new ProfileViewModel()
                {
                    User = user,
                    Email = user.Email,
                    ProfileImage = user.Thumbnail,
                    FirstName = user.Name,
                    LastName = user.LastName,
                    WallPosts = wallPosts.ToList(),
                    DateOfBirth = user.DateOfBirth,
                    //Followings = _followingRepository.GetFollowings(userId).ToLookup(a => a.FolloweeId),
                    //ShowActions = User.Identity.IsAuthenticated,
                    Description = user.Description,
                    FolloweesCount = _followingRepository.GetFolloweesCount(userId),
                    FollowersCount = _followingRepository.GetFollowersCount(userId)
                };

  return View("MyProfile", viewModel);
            }
        

        public ActionResult VisitProfile(string id)
        {
            var userId = User.Identity.GetUserId();
           
           var followeesIds= _context.Followings
                                 .Where(f => f.FollowerId == userId)
                                 .Select(f => f.FolloweeId)
                                 .ToList();
            var otherUserId = id;
                var user = _context.Users.Include(u => u.WallPosts)
                        .SingleOrDefault(u => u.Id == otherUserId);
                var wallPosts = _context.WallPosts
                    .Include(w => w.Post)
                    .Where(w => w.UserId == otherUserId)
                    .OrderByDescending(w => w.Post.Datetime); ;
                var viewModel = new ProfileViewModel()
                {
                    LoggedUserFollowingIds=followeesIds,
                    LoggedUserId=userId,
                    User = user,
                    Email = user.Email,
                    ProfileImage = user.Thumbnail,
                    FirstName = user.Name,
                    LastName = user.LastName,
                    WallPosts = wallPosts.ToList(),
                    DateOfBirth = user.DateOfBirth,
                    //Followings = _followingRepository.GetFollowings(otherUserId).ToLookup(a => a.FolloweeId),
                    //ShowActions = User.Identity.IsAuthenticated,
                    Description=user.Description,
                    IsAdmin=user.IsAdmin
                };
                return View(viewModel);           
        }

        
        

    }
}