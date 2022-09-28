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
        private readonly ApplicationDbContext _context;
        private readonly FollowingRepository _followingRepository;
        private readonly UserRepository _userRepository;
        private readonly WallPostRepository _wallPostRepository;

        public ProfileController()
        {
            _context = new ApplicationDbContext();
            _followingRepository = new FollowingRepository(_context);
            _userRepository = new UserRepository(_context);
            _wallPostRepository = new WallPostRepository(_context);
        }

        // GET: Profile
      
        [Authorize]
        public ActionResult MyProfile()
        {
         
                var userId = User.Identity.GetUserId();
                ViewBag.loggedUser = userId;
                 var user= _userRepository.GetById(userId);
                 ViewBag.LoggedUser = user;

            var wallposts = _wallPostRepository.GetByUser(userId);
               

                var viewModel = new ProfileViewModel()
                {
                    User = user,
                    Email = user.Email,
                    ProfileImage = user.Thumbnail,
                    FirstName = user.Name,
                    LastName = user.LastName,
                    WallPosts = wallposts.ToList(),
                    DateOfBirth = user.DateOfBirth,
                    Description = user.Description,
                    FolloweesCount = _followingRepository.GetFolloweesCount(userId),
                    FollowersCount = _followingRepository.GetFollowersCount(userId)
                };

                return View("MyProfile", viewModel);
            }
      
        public ActionResult VisitingProfile(string id)
        {
            var userId = User.Identity.GetUserId();
            var loggedUser = _userRepository.GetById(userId);
            ViewBag.LoggedUser = loggedUser;

            var followeesIds = _followingRepository.GetFolloweesIds(userId);

            var otherUserId = id;
            var user = _userRepository.GetById(otherUserId);

            var wallposts = _wallPostRepository.GetByUser(otherUserId);

            var viewModel = new ProfileViewModel()
            {
                LoggedUserFollowingIds = followeesIds,
                LoggedUserId = userId,
                User = user,
                Email = user.Email,
                ProfileImage = user.Thumbnail,
                FirstName = user.Name,
                LastName = user.LastName,
                WallPosts = wallposts.ToList(),
                DateOfBirth = user.DateOfBirth,
                Description = user.Description,
                IsAdmin = user.IsAdmin
            };
            return View(viewModel);
        }


    }
}