using GroupProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;
        // GET: Checkout

        public CheckoutController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.LoggedUser = _context.Users.SingleOrDefault(u => u.Id == userId);
            return View();
        }

        public ActionResult Success()
        {

            return View();
        }
    }
}