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

namespace GroupProject.Controllers
{
    public class TopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Topics
        public ActionResult Index()
        {
            return View(db.Topics.Include(t=>t.User).ToList());
        }

        // GET: Topics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Include(t=>t.User).SingleOrDefault(t=>t.Id==id);
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
                db.Topics.Add(topic);
                db.SaveChanges();
                var post = new Post()
                {
                    Body = viewModel.Post.Body,
                    Datetime = DateTime.Now
                };
                db.Posts.Add(post);
                db.SaveChanges();
                var topicPost = new TopicPost()
                {
                    PostId = post.Id,
                    SenderId = userId,
                    TopicId = topic.Id
                };
                db.TopicPosts.Add(topicPost);
                db.SaveChanges();
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
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Title,Created")] Topic topic)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(topic).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(topic);
        //}

        // GET: Topics/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Topic topic = db.Topics.Find(id);
        //    if (topic == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(topic);
        //}

        //// POST: Topics/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Topic topic = db.Topics.Find(id);
        //    db.Topics.Remove(topic);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
