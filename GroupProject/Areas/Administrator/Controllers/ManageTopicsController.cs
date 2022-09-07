using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
            return View(topics);
        }
    }
}