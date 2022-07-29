﻿using System;
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
    public class WallPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WallPosts
        public ActionResult Index()
        {
            var wallPosts = db.WallPosts.Include(w => w.Post).Include(w => w.User);
            return View(wallPosts.ToList());
        }

        // GET: WallPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WallPost wallPost = db.WallPosts.Find(id);
            if (wallPost == null)
            {
                return HttpNotFound();
            }
            return View(wallPost);
        }

        // GET: WallPosts/Create
        public ActionResult Create()
        {
            var viewModel = new WallPostViewModel();


            return View(viewModel);
        }

        // POST: WallPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( WallPostViewModel viewModel)
        {
            var post = new Post()
            {
                Body = viewModel.Body,
                Datetime = DateTime.Now
            };
            db.Posts.Add(post);
            db.SaveChanges();
            
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var wallPost = new WallPost()
                {
                    UserId = userId,
                    PostId = post.Id

                };
                db.WallPosts.Add(wallPost);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }           
            return View();
        }

        public ActionResult Wall()
        {
            var userId = User.Identity.GetUserId();

            var wallPosts = db.WallPosts
                .Include(w => w.Post)
                .Where(w => w.UserId == userId);

            return View(wallPosts);
        }

        // GET: WallPosts/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    WallPost wallPost = db.WallPosts.Find(id);
        //    if (wallPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.PostId = new SelectList(db.Posts, "Id", "Body", wallPost.PostId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Name", wallPost.UserId);
        //    return View(wallPost);
        //}

        //// POST: WallPosts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,UserId,PostId")] WallPost wallPost)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(wallPost).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.PostId = new SelectList(db.Posts, "Id", "Body", wallPost.PostId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Name", wallPost.UserId);
        //    return View(wallPost);
        //}

        //// GET: WallPosts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    WallPost wallPost = db.WallPosts.Find(id);
        //    if (wallPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(wallPost);
        //}

        //// POST: WallPosts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    WallPost wallPost = db.WallPosts.Find(id);
        //    db.WallPosts.Remove(wallPost);
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