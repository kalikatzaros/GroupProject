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
    public class TopicPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TopicPosts
     
        public ActionResult Index(int? id)
        {
            ViewBag.UserId = User.Identity.GetUserId();
            var topicPosts = db.TopicPosts.Include(t => t.Post)
                .Include(t => t.Sender)
                .Include(t => t.Topic)
                .Where(t=>t.TopicId==id)
                .OrderBy(t=>t.Post.Datetime);
            return View(topicPosts.ToList());
        }

        // GET: TopicPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicPost topicPost = db.TopicPosts.Find(id);
            if (topicPost == null)
            {
                return HttpNotFound();
            }
            return View(topicPost);
        }

        // GET: TopicPosts/Create
        public ActionResult Create(int id)
        {
            var viewModel = new TopicPostViewModel();
          
            

            return View(viewModel);
        }

        // POST: TopicPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TopicPostViewModel viewModel,int id)
        {
            var post = new Post()
            {
                Body=viewModel.Body,
                Datetime=DateTime.Now
            };
            db.Posts.Add(post);
            db.SaveChanges();

            var userId = User.Identity.GetUserId();

                var topicPost = new TopicPost()
                {
                    TopicId = id,
                    SenderId = userId,
                    PostId = post.Id
                };

                db.TopicPosts.Add(topicPost);
                db.SaveChanges();
            return RedirectToAction("Index", "TopicPosts",new {id=id });
       
        }

        // GET: TopicPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicPost topicPost = db.TopicPosts.Find(id);
            if (topicPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Body", topicPost.PostId);
            ViewBag.SenderId = new SelectList(db.Users, "Id", "Name", topicPost.SenderId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Title", topicPost.TopicId);
            return View(topicPost);
        }

        // POST: TopicPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TopicId,SenderId,PostId")] TopicPost topicPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topicPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Body", topicPost.PostId);
            ViewBag.SenderId = new SelectList(db.Users, "Id", "Name", topicPost.SenderId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Title", topicPost.TopicId);
            return View(topicPost);
        }

        // GET: TopicPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicPost topicPost = db.TopicPosts.Find(id);
            if (topicPost == null)
            {
                return HttpNotFound();
            }
            return View(topicPost);
        }

        // POST: TopicPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TopicPost topicPost = db.TopicPosts.Find(id);
            db.TopicPosts.Remove(topicPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
