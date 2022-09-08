using GroupProject.Models;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    [Authorize]
    public class NewsFeedController : Controller
    {
        private readonly ApplicationDbContext _context;
        // GET: NewsFeed
        public NewsFeedController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var loggedUserId= User.Identity.GetUserId();
            var peopleIFollow = _context.Followings
                .Where(f => f.FollowerId == loggedUserId)
                .Select(f => f.FolloweeId).ToList();
            var newsfeedViewModel = new NewsFeedViewModel() {
                Wallposts = new List<WallPost>(),
                TopicPosts=new List<TopicPost>()
            };
           foreach(var id in peopleIFollow)
            {
                var lastWallPost = _context.WallPosts
                    .Include(w => w.Post)
                    .Include(w => w.User)
                    .Where(w => w.UserId == id&&w.User.IsDeactivated==false).ToList().LastOrDefault();

                var lastTopicPost = _context.TopicPosts
                   .Include(w => w.Topic)
                   .Include(w => w.Post)
                   .Include(w => w.Sender)
                   .Where(w => w.Sender.Id == id&&w.Sender.IsDeactivated==false).ToList().LastOrDefault();

                if (lastWallPost != null) 
                {
                    newsfeedViewModel.Wallposts.Add(lastWallPost); 
                }

                if(lastTopicPost!=null)
                {
                    newsfeedViewModel.TopicPosts.Add(lastTopicPost);
                }

            }
            return View(newsfeedViewModel);
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