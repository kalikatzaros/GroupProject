using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroupProject.Repositories
{
    public class PostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _context.Posts.Add(post);

            Save();
        }

        public Post GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return _context.Posts
               
                .SingleOrDefault(p => p.Id == id);
        }

        public void Update(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _context.Entry(post).State = EntityState.Modified;
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