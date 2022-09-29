using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext _context ;
        private readonly TopicRepository _topicRepository;
        private readonly TopicPostRepository _topicPostRepository;
        private readonly UserRepository _userRepository;
        private readonly PostRepository _postRepository;

        public TopicsController()
        {
            _context = new ApplicationDbContext();
            _topicRepository = new TopicRepository(_context);
            _topicPostRepository = new TopicPostRepository(_context);
            _userRepository = new UserRepository(_context);
            _postRepository = new PostRepository(_context);
        }
      

        public ActionResult GetTopics()
        {
            var userId = User.Identity.GetUserId();
            //var roleId = _context.Roles.Where(r => r.Name == "Admin").Select(r => r.Id).SingleOrDefault();
            var loggedUser = _userRepository.GetById(userId);
            ViewBag.LoggedUser = loggedUser;
            var topics = _topicRepository.GetAll().ToList();
            return View(topics);
        }
        // GET: Topics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = _topicRepository.GetById(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topics/Create
        public ActionResult Create()
        {
            var viewModel = new TopicViewModel();
            return View(viewModel);
        }

        // POST: Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TopicViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            var topic = new Topic()
            {
                Title=viewModel.Title,
                Created=DateTime.Now,
                UserId=userId
            };
            if (ModelState.IsValid)
            {
                _topicRepository.Create(topic);
               
                var post = new Post()
                {
                    Body = viewModel.Post.Body,
                    Datetime = DateTime.Now
                };
                _postRepository.Create(post);
               
                var topicPost = new TopicPost()
                {
                    PostId = post.Id,
                    SenderId = userId,
                    TopicId = topic.Id
                };
                _topicPostRepository.Create(topicPost);
               
                return RedirectToAction("Index");
            }

            return View(topic);
        }

        // GET: Topics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = _topicRepository.GetById(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _topicRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
