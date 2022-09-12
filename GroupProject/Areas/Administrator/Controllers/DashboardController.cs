using GroupProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Areas.Administrator.Controllers
{
    [Authorize(Roles ="Admin")]
   
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
       

        // GET: Administrator/Dashboard
        public DashboardController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index() {
           
            var userId = User.Identity.GetUserId();
            ViewBag.loggedUser = _context.Users.SingleOrDefault(u => u.Id == userId);
            var users = _context.Users
                       .Where(u => u.Id != userId).ToList();
            return View(users);
        }
      
        public ActionResult GetAllWallPosts()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.loggedUser = _context.Users.SingleOrDefault(u => u.Id == userId);
            var wallposts = _context.WallPosts
                .Include(w=>w.Post)
                .Include(w=>w.User)
                .ToList();
            return View(wallposts);
        }

        public ActionResult ViewProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            ViewBag.loggedUser = user;
            return View(user);
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