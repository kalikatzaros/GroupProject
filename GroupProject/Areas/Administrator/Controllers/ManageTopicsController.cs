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
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;
namespace GroupProject.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageTopicsController : Controller
    {
        private readonly ApplicationDbContext _context;
        // GET: Administrator/ManageTopics
        public ManageTopicsController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var topics = _context.Topics
                .Include(t => t.User)
                .ToList();
            var topicPosts = _context.TopicPosts
                .Include(tp => tp.Post)
                .Include(tp=>tp.Topic)
                .Include(tp=>tp.Sender)
                .ToList();
            var viewModel = new TopicsViewModel() 
            {
              Topics=topics,
              TopicPosts=topicPosts
            };
            return View(viewModel);
        }
    }
}