using GroupProject.Models;
using GroupProject.ViewModels;
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


        public ActionResult GetPeopleIFollow(string search)
        {
            string userId = User.Identity.GetUserId();
            var viewModel = new SearchPeopleViewModel();
            if (search == null)
            {
                var followees = _context.Followings
                            .Where(f => f.FollowerId == userId && f.Followee.IsDeactivated == false)
                            .Select(f => f.Followee)
                            .ToList();
               
                viewModel.Followees = followees;
                return View(viewModel);
            }
            else
            {
                var followees = _context.Followings
                                  .Where(f => f.FollowerId == userId && f.Followee.IsDeactivated == false && (f.Followee.Name.ToLower() == search.ToLower()
                         || f.Followee.LastName.ToLower() == search.ToLower()
                         || f.Followee.Email.ToLower() == search.ToLower()))
                                  .Select(f => f.Followee)
                               .ToList();
             
                viewModel.Followees = followees;
                return View(viewModel);
            }
    
        }












    }
}