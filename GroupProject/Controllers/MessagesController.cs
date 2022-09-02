using GroupProject.Models;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

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
            return View(_context.Messages.ToList());
        }

        public ActionResult CreateMessage()
        {
            var viewModel = new UserMessagesViewModel();
            var userId = User.Identity.GetUserId();

            viewModel.Users = _context.Users.Where(u => u.Id != userId);
            return View(viewModel);
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreateMessage(UserMessagesViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            
            var message = new Message()
            {
                SenderId = userId,
                ReceiverId = viewModel.Message.ReceiverId,
                Body = viewModel.Message.Body,
                Datetime = DateTime.Now
            };
            _context.Messages.Add(message);
            _context.SaveChanges();
            return RedirectToAction("Index", "NewsFeed");
        }
        [Authorize]
        public ActionResult ReadIncomingMessages()
        {
            var id = User.Identity.GetUserId();
            var loggedUser = _context.Users.SingleOrDefault(u => u.Id == id);
            ViewBag.Thumbnail = loggedUser.Thumbnail;
            ViewBag.Email = loggedUser.Email;
            var messages = _context.Messages
                            .Include("Sender")
                            .Include("Receiver")
                            .Where(m => m.ReceiverId == id);
            return View(messages);
        }

        public ActionResult ReadSentMessages()
        {
            var id = User.Identity.GetUserId();
            var loggedUser = _context.Users.SingleOrDefault(u => u.Id == id);
            ViewBag.Thumbnail = loggedUser.Thumbnail;
            ViewBag.Email = loggedUser.Email;
            var messages = _context.Messages
                            .Include("Sender")
                            .Include("Receiver")
                            .Where(m => m.SenderId == id);
                           
            return View(messages);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var message = _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .SingleOrDefault(m => m.Id == id);
            if(message == null)
            {
                return HttpNotFound();
            }
            var userId = User.Identity.GetUserId();
            ViewBag.userId = userId;
            if (message.IsRead == false)
            {
                message.IsRead = true;
                _context.Entry(message).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return View(message);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = _context.Messages
                            .Include("Receiver")
                            .Include("Sender")
                            .SingleOrDefault(m => m.Id == id);
            
            if (message == null)
            {
                return HttpNotFound();
            }
            
            return View(message);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            var message = _context.Messages.SingleOrDefault(m => m.Id == id);

            _context.Messages.Remove(message);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


    }
}