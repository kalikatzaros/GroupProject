﻿using GroupProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly ApplicationDbContext _context;


        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult GetPeopleIFollow()
        {
            string userId = User.Identity.GetUserId();
            var followees = _context.Followings
                                .Where(f => f.FollowerId == userId)
                                .Select(f => f.Followee)
                                .ToList();

            return View(followees);
        }

       
    }
}