using GroupProject.Models;
using GroupProject.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
       public ActionResult GetAllPeople()
        {
            var userId = User.Identity.GetUserId();
            //var roleId = _context.Roles.Where(r => r.Name == "Admin").Select(r => r.Id).SingleOrDefault();
            var followees = _context.Followings
                                .Where(f => f.FollowerId == userId && f.Followee.IsDeactivated == false)
                                .Select(f => f.Followee)
                                .ToList();

            var followers = _context.Followings
                .Where(f => f.FolloweeId == userId && f.Follower.IsDeactivated == false)
                .Select(f => f.Follower)
                .ToList();

            var users = _context.Users
                    .Where(u => u.Id != userId && u.IsDeactivated == false).ToList();
            //.Where(u => u.Id != userId&&u.IsDeactivated==false&& !(u.Roles.Any(r => r.RoleId == roleId))).ToList();
            var peopleViewModel = new PeopleViewModel()
            {
                AllUsers = users,
                Followees = followees,
                Followers = followers,
                UserId = userId
            };
            return View(peopleViewModel);
        }
        public ActionResult Index(string search)
        {
            var userId = User.Identity.GetUserId();
            //var roleId = _context.Roles.Where(r => r.Name == "Admin").Select(r => r.Id).SingleOrDefault();
            var followees = _context.Followings
                                .Where(f => f.FollowerId == userId&&f.Followee.IsDeactivated==false)
                                .Select(f => f.Followee)
                                .ToList();

            var followers = _context.Followings
                .Where(f => f.FolloweeId == userId&&f.Follower.IsDeactivated==false)
                .Select(f => f.Follower)
                .ToList();
           
            if (search == null)
            {
                var users = _context.Users
                     .Where(u => u.Id != userId && u.IsDeactivated == false).ToList();
                    //.Where(u => u.Id != userId&&u.IsDeactivated==false&& !(u.Roles.Any(r => r.RoleId == roleId))).ToList();
                var peopleViewModel = new PeopleViewModel()
                {
                    AllUsers = users,
                    Followees = followees,
                    Followers = followers,
                    UserId = userId
                };
                return View(peopleViewModel);
            }
            else
            {
                var users = _context.Users
                         .Where(u => u.Id != userId && (u.Name.ToLower() == search.ToLower()
                         || u.LastName.ToLower() == search.ToLower()
                         || u.Email.ToLower() == search.ToLower())&&u.IsDeactivated==false)
                         .ToList();
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

        public ActionResult GetPeople(string search)
        {
            var userId = User.Identity.GetUserId();
            //var roleId = _context.Roles.Where(r => r.Name == "Admin").Select(r => r.Id).SingleOrDefault();
           

            ViewBag.LoggedUser = _context.Users.SingleOrDefault(u => u.Id == userId);

            var followees = _context.Followings
                                .Where(f => f.FollowerId == userId && f.Followee.IsDeactivated == false)
                                .Select(f => f.Followee)
                                .ToList();

            var followers = _context.Followings
                .Where(f => f.FolloweeId == userId && f.Follower.IsDeactivated == false)
                .Select(f => f.Follower)
                .ToList();

            if (search == null)
            {
                var users = _context.Users
                     .Where(u => u.Id != userId && u.IsDeactivated == false).ToList();
                //.Where(u => u.Id != userId&&u.IsDeactivated==false&& !(u.Roles.Any(r => r.RoleId == roleId))).ToList();
                var peopleViewModel = new PeopleViewModel()
                {
                    AllUsers = users,
                    Followees = followees,
                    Followers = followers,
                    UserId = userId
                };
                return View(peopleViewModel);
            }
            else
            {
                var users = _context.Users
                         .Where(u => u.Id != userId && (u.Name.ToLower() == search.ToLower()
                         || u.LastName.ToLower() == search.ToLower()
                         || u.Email.ToLower() == search.ToLower()) && u.IsDeactivated == false)
                         .ToList();
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
}