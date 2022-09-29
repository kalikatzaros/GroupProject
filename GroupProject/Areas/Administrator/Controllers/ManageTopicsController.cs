using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GroupProject.Areas.Administrator.ViewModels;
using GroupProject.Models;
using GroupProject.Repositories;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;

namespace GroupProject.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageTopicsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserRepository _userRepository;
        private readonly TopicRepository _topicRepository;
        private readonly TopicPostRepository _topicPostRepository;

        // GET: Administrator/ManageTopics
        public ManageTopicsController()
        {
            _context = new ApplicationDbContext();
            _userRepository = new UserRepository(_context);
            _topicRepository = new TopicRepository(_context);
            _topicPostRepository = new TopicPostRepository(_context);
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var loggedUser = _userRepository.GetById(userId);
            ViewBag.loggedUser = loggedUser;
            //ViewBag.loggedUser = _context.Users.SingleOrDefault(u => u.Id == userId);

            var topics = _topicRepository.GetAll();
            //var topics = _context.Topics
            //    .Include(t => t.User)
            //    .ToList();

            var topicPosts = _topicPostRepository.GetAll();
            //var topicPosts = _context.TopicPosts
            //    .Include(tp => tp.Post)
            //    .Include(tp=>tp.Topic)
            //    .Include(tp=>tp.Sender)
            //    .ToList();

            var viewModel = new TopicsViewModel() 
            {
              Topics=topics.ToList(),
              TopicPosts=topicPosts.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UpdateTopic(int id,string title)
        {
            var userId = User.Identity.GetUserId();
            var loggedUser = _userRepository.GetById(userId);
            ViewBag.loggedUser = loggedUser;

            var topic = _topicRepository.GetById(id);
            //var topic = _context.Topics
            //                  .SingleOrDefault(t => t.Id == id);
            topic.Title = title;

            _topicRepository.Update(topic);

            //_context.Entry(topic).State = EntityState.Modified;
            //_context.SaveChanges();
            return RedirectToAction("Index", "ManageTopics");
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