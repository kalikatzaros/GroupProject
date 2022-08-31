﻿using GroupProject.Models;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PeopleController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: People

        //public ActionResult Index(string search)
        //{
        //    var userId = User.Identity.GetUserId();

        //    if (search == null)
        //    {
        //        var users = _context.Users
        //            .Include(u=>u.Followees)
        //            .Include(u =>u.Followers)
        //            .Where(u => u.Id != userId).ToList();
        //        ViewBag.userId = userId;
        //        return View(users);
        //    }
            

        //    var searched = _context.Users
        //                 .Include(u => u.Followees)
        //                 .Include(u => u.Followers)
        //                 .Where(u => u.Id != userId &&( u.Name.ToLower() == search.ToLower() 
        //                 || u.LastName.ToLower() == search.ToLower()
        //                 || u.Email.ToLower()== search.ToLower()))
        //                 .ToList();
        //    ViewBag.userId = userId;
        //    return View(searched);
            
        //}



        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var followees = _context.Followings
                                .Where(f => f.FollowerId == userId)
                                .Select(f => f.Followee)
                                .ToList();

            var followers = _context.Followings
                .Where(f => f.FolloweeId == userId)
                .Select(f => f.Follower)
                .ToList();

            var users = _context.Users
                    .Where(u => u.Id != userId).ToList();
            //ViewBag.userId = userId;

            //var unknownUsers = _context.Users
            //        .Where(u => u.Followees.Contains(userId)).ToList();

            var peopleViewModel = new PeopleViewModel()
            {
                AllUsers = users,
                Followees = followees,
                Followers = followers,
                UserId = userId
            };
            return View(peopleViewModel);
        }
    }
}