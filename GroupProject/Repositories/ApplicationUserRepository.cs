using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace GroupProject.Repositories
{
    public class ApplicationUserRepository : IDisposable
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository()
        {
            _context = new ApplicationDbContext();

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }        

    }
}