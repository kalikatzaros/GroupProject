﻿using GroupProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class FollowersController : Controller
    {
        private readonly ApplicationDbContext _context;


        public FollowersController()
        {
            _context = new ApplicationDbContext();
        }
              
        public ActionResult GetPeopleThatFollowMe()
        {
            var userId = User.Identity.GetUserId();
            var followers = _context.Followings
                .Where(f => f.FolloweeId == userId&&f.Follower.IsDeactivated==false)
                .Select(f => f.Follower)
                .ToList();
            return View(followers);
        }
    }
}