using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GroupProject.Controllers.API
{
    [Authorize]
    [RoutePrefix("api/users")]
    public class ManageUsersController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public ManageUsersController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        [Route("toggleActivate/{id}")]
        public IHttpActionResult ToggleActivate(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
           
            var user = _context.Users
                 .SingleOrDefault(u => u.Id == id);
                if (user.IsDeactivated)
                {
                user.IsDeactivated = false;
                user.LockoutEndDateUtc = DateTime.UtcNow;
                    _context.Entry(user).State = EntityState.Modified;
                    _context.SaveChanges();
            }
            else
            {
                user.IsDeactivated = true;
                user.LockoutEndDateUtc = DateTime.MaxValue;
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }

                return Ok(user);
            
            
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
