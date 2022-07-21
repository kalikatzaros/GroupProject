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
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Messages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateMessage()
        {
            var viewModel = new UserMessagesViewModel();
            viewModel.Users = _context.Users;
            return View(viewModel);
        }




    }
}