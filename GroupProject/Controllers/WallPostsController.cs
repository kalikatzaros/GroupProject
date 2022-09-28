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
    public class WallPostsController : Controller
    {
        private ApplicationDbContext _context;
        private WallPostRepository _repository;

        public WallPostsController()
        {
            _context = new ApplicationDbContext();
            _repository = new WallPostRepository(_context);
        }
     
      
        // POST: WallPosts/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( WallPostViewModel viewModel)
        {
            var post = new Post()
            {
                Body = viewModel.Body,
                Datetime = DateTime.Now,
                
            };

            if (viewModel.ImageFile != null)
            {
                post.Thumbnail = Path.GetFileName(viewModel.ImageFile.FileName);
                string fullPath = Path.Combine(Server.MapPath("~/img"), post.Thumbnail);
                viewModel.ImageFile.SaveAs(fullPath);
            }
            

            _context.Posts.Add(post);
           _context.SaveChanges();
            
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var wallPost = new WallPost()
                {
                    UserId = userId,
                    PostId = post.Id

                };
               _context.WallPosts.Add(wallPost);
                _context.SaveChanges();
                return RedirectToAction("NewsFeed", "NewsFeed");
            }           
            return View();
        }

        // GET: WallPosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WallPostViewModel viewModel)
        {
            var wallpost = _context.WallPosts
                .Include(wp => wp.Post)
                .SingleOrDefault(wp => wp.Id == viewModel.Id);
            wallpost.Post.Body = viewModel.Body;
            wallpost.Post.Datetime = DateTime.Now;
            if (viewModel.ImageFile != null)
            {
                wallpost.Post.Thumbnail = Path.GetFileName(viewModel.ImageFile.FileName);
                string fullPath = Path.Combine(Server.MapPath("~/img"), wallpost.Post.Thumbnail);
                viewModel.ImageFile.SaveAs(fullPath);
            }
            if (ModelState.IsValid)
            {
               _context.Entry(wallpost).State = EntityState.Modified;
                
                _context.SaveChanges();
                return RedirectToAction("MyProfile","Profile");
            }      
            return View(wallpost);
        }

        // GET: WallPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WallPost wallPost = _context.WallPosts.Find(id);
            if (wallPost == null)
            {
                return HttpNotFound();
            }
            return View(wallPost);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
