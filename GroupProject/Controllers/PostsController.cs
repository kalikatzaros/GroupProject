using GroupProject.Models;
using Microsoft.AspNet.Identity;
using GroupProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using GroupProject.ViewModel;

namespace GroupProject.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Posts
        public ActionResult Index()
        {             
            return View(_context.Posts.ToList());
        }

        public ActionResult CreatePost()
        {
            var viewModel = new UserPostsViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreatePost(UserPostsViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            var post = new Post()
            {               
                SenderId = userId,
                Body = viewModel.Post.Body,
                Datetime = DateTime.Now
            };
            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Index","Home");
        }

    }
}