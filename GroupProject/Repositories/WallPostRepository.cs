using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace GroupProject.Repositories
{
    public class WallPostRepository
    {
        private readonly ApplicationDbContext _context;

        public WallPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<WallPost> GetAll()
        {
            return _context.WallPosts
                .Include(w => w.Post)
                .Include(w => w.User);
        }
       public WallPost GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return _context.WallPosts
                .Include(wp => wp.Post)
                .Include(wp => wp.User)
                .SingleOrDefault(wp => wp.Id == id);
        }

        public IEnumerable<WallPost> GetByUser(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.WallPosts
               .Include(wp => wp.Post)
               .Include(wp => wp.User)
               .Where(wp => wp.UserId == id && wp.User.IsDeactivated == false)
               .OrderByDescending(w => w.Post.Datetime);
               

        }

        public void Create(WallPost wallpost)
        {
            if (wallpost == null)
                throw new ArgumentNullException(nameof(wallpost));

            _context.WallPosts.Add(wallpost);

            Save();
        }

        public void Update(WallPost wallpost)
        {
            if (wallpost == null)
                throw new ArgumentNullException(nameof(wallpost));

            _context.Entry(wallpost).State = EntityState.Modified;
            Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            WallPost wallpost = GetById(id);

            if(wallpost==null)
                throw new Exception("Not found");

            _context.WallPosts.Remove(wallpost);
            Save();

        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}