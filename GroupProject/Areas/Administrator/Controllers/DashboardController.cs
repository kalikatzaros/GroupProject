using GroupProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
       
        //private readonly UserManager<ApplicationUser> _userManager;
        // GET: Administrator/Dashboard
        public DashboardController()
        {
            _context = new ApplicationDbContext();
            //_userManager = new UserManager<ApplicationUser>();
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var users = _context.Users
                       .Where(u => u.Id != userId).ToList();
            return View(users);
        }
        public ActionResult DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _context.Users
                           
                            .SingleOrDefault(m => m.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = _context.Users.SingleOrDefault(m => m.Id == id);

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }
    }
}