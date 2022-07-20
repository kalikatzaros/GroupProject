using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupProject.Repositories;



namespace GroupProject.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationUserRepository _repo;


        public ApplicationUserController()
        {
            _repo = new ApplicationUserRepository();
        }
        
        // GET: ApplicationUser
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ActionName()
        {
            string userId = User.Identity.GetUserId();
            return View();
        }
    }
}