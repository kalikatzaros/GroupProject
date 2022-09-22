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
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;

namespace GroupProject.Controllers
{
    public class TopicPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TopicPosts
     
        //public ActionResult Index(int? id)
        //{
        //    var userId = User.Identity.GetUserId();
        //    ViewBag.userId = userId;
        //    var topicPosts = db.TopicPosts.Include(t => t.Post)
        //        .Include(t => t.Sender)
        //        .Include(t => t.Topic)
        //        .Where(t=>t.TopicId==id)
        //        .OrderBy(t=>t.Post.Datetime);
        //    return View(topicPosts.ToList());
        //}
        public ActionResult GetTopicPosts(int id)
        {
            //if (id == null)
            //{
            //    return View("Error");
            //}
            var userId = User.Identity.GetUserId();
           
            //var roleId = _context.Roles.Where(r => r.Name == "Admin").Select(r => r.Id).SingleOrDefault();

            var loggedUser= db.Users.SingleOrDefault(u => u.Id == userId);
            ViewBag.LoggedUser = loggedUser;
            var topic = db.Topics.SingleOrDefault(t => t.Id == id);
            var topicPosts = db.TopicPosts.Include(t => t.Post)
                .Include(t => t.Sender)
                .Include(t => t.Topic)
                .Where(t => t.TopicId == id)
                .OrderBy(t => t.Post.Datetime).ToList();
            var viewModel = new GetTopicPostsViewModel() { 
            LoggedInUser=loggedUser,
            TopicPosts=topicPosts,
            Topic=topic,
            TopicId=id
                       };

            return View(viewModel);
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(TopicPostViewModel viewModel)
        //{
        //    var post = new Post()
        //    {
        //        Body=viewModel.Body,
        //        Datetime=DateTime.Now
        //    };

        //    if (viewModel.ImageFile != null)
        //    {
        //        post.Thumbnail = Path.GetFileName(viewModel.ImageFile.FileName);
        //        string fullPath = Path.Combine(Server.MapPath("~/img"), post.Thumbnail);
        //        viewModel.ImageFile.SaveAs(fullPath);
        //    }

        //    db.Posts.Add(post);
        //    db.SaveChanges();

        //    var userId = User.Identity.GetUserId();

        //        var topicPost = new TopicPost()
        //        {
        //            //TopicId = id,
        //            TopicId=viewModel.TopicId,
        //            SenderId = userId,
        //            PostId = post.Id
        //        };

        //        db.TopicPosts.Add(topicPost);
        //        db.SaveChanges();
        //    return RedirectToAction("GetTopicPosts", "TopicPosts",new {id= viewModel.TopicId });
        //    //return RedirectToAction("Index", "TopicPosts", new { id = id });
        //}

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

            db.Posts.Add(post);
            db.SaveChanges();

            var userId = User.Identity.GetUserId();


          
            var topicPost = new TopicPost()
            {
               TopicId = viewModel.TopicId,
                SenderId = userId,
                PostId = post.Id
            };

            db.TopicPosts.Add(topicPost);
            db.SaveChanges();
            return RedirectToAction("GetTopicPosts", "TopicPosts", new { id = viewModel.TopicId });
            //return RedirectToAction("Index", "TopicPosts", new { id = id });
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
        public ActionResult Edit(string body,int? id)
        {
            var topicPost = db.TopicPosts
                .Include(tp=>tp.Post)
                .Include(tp => tp.Topic)
                .SingleOrDefault(tp => tp.Id == id);

            topicPost.Post.Body = body;
            topicPost.Post.Datetime = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(topicPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "TopicPosts", new { id = topicPost.Topic.Id });
            }
            return View("Error");
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
