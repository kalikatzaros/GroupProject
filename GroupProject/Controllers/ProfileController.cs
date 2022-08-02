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
    public class ProfileController : Controller
    {
        private ApplicationDbContext _context;

        public ProfileController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Profile
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            var viewModel = new ProfileViewModel()
            {
                User = user,
                Email = user.Email,
                ProfileImage =user.Thumbnail,
                FirstName = user.Name,
                LastName = user.LastName
            };
            return View(viewModel);
        }

        public ActionResult GetProfileImage()
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            return View(user.Thumbnail);
        }
    }
}