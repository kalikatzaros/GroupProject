using GroupProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class ChatRoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatRoomController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: ChatRoom
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignalRChat()
        {

            var userId = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            ViewBag.UserName = user.FullName;

            return View();
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