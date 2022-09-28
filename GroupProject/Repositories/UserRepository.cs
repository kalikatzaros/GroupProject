using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Repositories
{
   
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetById(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return _context.Users.SingleOrDefault(u => u.Id == id);
        }

        
    }
}