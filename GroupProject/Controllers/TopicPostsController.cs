using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GroupProject.Models;
using GroupProject.Repositories;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;

namespace GroupProject.Controllers
{
    public class TopicPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly TopicRepository _topicRepository;
        private readonly TopicPostRepository _topicPostRepository;
        private readonly UserRepository _userRepository;
        private readonly PostRepository _postRepository;

        public TopicPostsController()
        {
            _context = new ApplicationDbContext();
            _topicRepository = new TopicRepository(_context);
            _topicPostRepository = new TopicPostRepository(_context);
            _userRepository = new UserRepository(_context);
            _postRepository = new PostRepository(_context);
        }

       
        public ActionResult GetTopicPosts(int id)
        {
           
            var userId = User.Identity.GetUserId();
           
           
           var loggedUser=_userRepository.GetById(userId);
            ViewBag.LoggedUser = loggedUser;

            var topic = _topicRepository.GetById(id);

            var topicPosts = _topicPostRepository.GetAll(id);

           

            var viewModel = new GetTopicPostsViewModel() { 
            LoggedInUser=loggedUser,
            TopicPosts=topicPosts.ToList(),
            Topic=topic,
            TopicId=id
                       };

            return View(viewModel);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTopicPost(GetTopicPostsViewModel viewModel)
        {
            var post = new Post()
            {
                Body = viewModel.Body,
                Datetime = DateTime.Now
            };

            if (viewModel.ImageFile != null)
            {
                post.Thumbnail = Path.GetFileName(viewModel.ImageFile.FileName);
                string fullPath = Path.Combine(Server.MapPath("~/img"), post.Thumbnail);
                viewModel.ImageFile.SaveAs(fullPath);
            }

            _postRepository.Create(post);
           

            var userId = User.Identity.GetUserId();


          
            var topicPost = new TopicPost()
            {
                TopicId = viewModel.TopicId,
                SenderId = userId,
                PostId = post.Id
            };

            _topicPostRepository.Create(topicPost);
          
            return RedirectToAction("GetTopicPosts", "TopicPosts", new { id = viewModel.TopicId });
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTopicPost(GetTopicPostsViewModel viewModel)
        {
            var topicPost = _topicPostRepository.GetById(viewModel.Id);

            topicPost.Post.Body = viewModel.Body;
            topicPost.Post.Datetime = DateTime.Now;

            if (viewModel.ImageFile != null)
            {
                topicPost.Post.Thumbnail = Path.GetFileName(viewModel.ImageFile.FileName);
                string fullPath = Path.Combine(Server.MapPath("~/img"), topicPost.Post.Thumbnail);
                viewModel.ImageFile.SaveAs(fullPath);
            }

            

            if (ModelState.IsValid)
            {
                _topicPostRepository.Update(topicPost);

                return RedirectToAction("GetTopicPosts", "TopicPosts", new { id = viewModel.TopicId });
            }


            return RedirectToAction("GetTopicPosts", "TopicPosts", new { id = viewModel.TopicId });


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
