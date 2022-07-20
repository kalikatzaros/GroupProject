<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Repositories
{
    public class ApplicationUserRepository
    {
    }
}
=======
﻿using GroupProject.Models;
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
>>>>>>> ec39783e1852e10eeab651b6d2170c6fb7c025c9
