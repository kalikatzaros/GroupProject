using GroupProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var users = _context.Users
                       .Where(u => u.Id != userId).ToList();
            return View(users);
        }
    }
}