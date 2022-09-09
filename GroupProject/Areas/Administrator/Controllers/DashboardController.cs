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
            var users = _context.Users
                       .Where(u => u.Id != userId).ToList();
            return View(users);
        }
      
        public ActionResult GetAllWallPosts()
        {
            var wallposts = _context.WallPosts
                .Include(w=>w.Post)
                .Include(w=>w.User)
                .ToList();
            return View(wallposts);
        }

    }
}