using GroupProject.Models;
using GroupProject.Repositories;
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
        private readonly TopicRepository _topicRepository;
        private readonly TopicPostRepository _topicPostRepository;
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;
        private readonly FollowingRepository _followingRepository;
        private readonly WallPostRepository _wallPostRepository;

        public NewsFeedController()
        {
            _context = new ApplicationDbContext();
            _topicRepository = new TopicRepository(_context);
            _topicPostRepository = new TopicPostRepository(_context);
            _postRepository = new PostRepository(_context);
            _userRepository = new UserRepository(_context);
            _followingRepository = new FollowingRepository(_context);
            _wallPostRepository = new WallPostRepository(_context);
        }

        public ActionResult Newsfeed()
        {
            var loggedUserId = User.Identity.GetUserId();
            var loggedUser = _userRepository.GetById(loggedUserId);
            ViewBag.LoggedUser = loggedUser;

            //var peopleIFollow = _context.Followings
            //    .Where(f => f.FollowerId == loggedUserId)
            //    .Select(f => f.FolloweeId).ToList();
            var peopleIFollow = _followingRepository.GetFolloweesIds(loggedUserId);

            //var users = _context.Users
            //       .Where(u => u.Id != loggedUserId
            //       && u.IsDeactivated == false).ToList();
            var users = _userRepository.GetAll(loggedUserId).ToList();

            var newsfeedViewModel = new NewsFeedViewModel()
            {
                Wallposts = new List<WallPost>(),
                TopicPosts = new List<TopicPost>(),
                People = users
            };

            foreach (var id in peopleIFollow)
            {
                //var lastWallPost = _context.WallPosts
                //    .Include(w => w.Post)
                //    .Include(w => w.User)
                //    .Where(w => w.UserId == id && w.User.IsDeactivated == false).ToList().LastOrDefault();

                var lastWallPost = _wallPostRepository.GetByUser(id).ToList().LastOrDefault();

                //var lastTopicPost = _context.TopicPosts
                //   .Include(w => w.Topic)
                //   .Include(w => w.Post)
                //   .Include(w => w.Sender)
                //   .Where(w => w.Sender.Id == id && w.Sender.IsDeactivated == false).ToList().LastOrDefault();

                var lastTopicPost = _topicPostRepository.GetByUser(id).ToList().LastOrDefault();

                if (lastWallPost != null)
                {
                    newsfeedViewModel.Wallposts.Add(lastWallPost);
                }

                if (lastTopicPost != null)
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
                _userRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}