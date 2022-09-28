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
        private readonly ApplicationDbContext _context;
        private readonly WallPostRepository _wallpostRepository;
        private readonly PostRepository _postRepository;

        public WallPostsController()
        {
            _context = new ApplicationDbContext();
            _wallpostRepository = new WallPostRepository(_context);
            _postRepository = new PostRepository(_context);
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
            

            _postRepository.Create(post);
           
            
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var wallPost = new WallPost()
                {
                    UserId = userId,
                    PostId = post.Id

                };
                _wallpostRepository.Create(wallPost);
                return RedirectToAction("NewsFeed", "NewsFeed");
            }           
            return View();
        }

        // GET: WallPosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WallPostViewModel viewModel)
        {
            var wallpost = _wallpostRepository.GetById(viewModel.Id);
     
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
                _wallpostRepository.Update(wallpost);
              
                return RedirectToAction("MyProfile","Profile");
            }      
            return View(wallpost);
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _wallpostRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
