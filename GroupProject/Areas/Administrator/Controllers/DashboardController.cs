using GroupProject.Models;
using GroupProject.Repositories;
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
        private readonly WallPostRepository _wallpostRepository;
        private readonly UserRepository _userRepository;

        // GET: Administrator/Dashboard
        public DashboardController()
        {
            _context = new ApplicationDbContext();
            _wallpostRepository = new WallPostRepository(_context);
            _userRepository = new UserRepository(_context);
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
            var loggedUser = _userRepository.GetById(userId);

            ViewBag.loggedUser = loggedUser;

            var wallposts = _wallpostRepository.GetAll();
            
            return View(wallposts);
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _wallpostRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}