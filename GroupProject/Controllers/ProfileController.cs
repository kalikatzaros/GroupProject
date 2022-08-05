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

namespace GroupProject.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext _context;

        public ProfileController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Profile
        public ActionResult Index(string Id)
        {
            if (Id == null)
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
                    DateOfBirth = user.DateOfBirth

                };
                return View(viewModel);
            }

            else
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
                    DateOfBirth = user.DateOfBirth

                };
                return View(viewModel);

            }
            



        }

        
        

    }
}