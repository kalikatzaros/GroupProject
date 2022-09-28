﻿using GroupProject.Models;
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

        public IEnumerable<WallPost> GetByUser(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.WallPosts
               .Include(wp => wp.Post)
               .Include(wp => wp.User)
               .Where(wp => wp.Id == id)
               .OrderByDescending(w => w.Post.Datetime)
               .ToList();

        }

        public void Create(WallPost wallpost)
        {
            if (wallpost == null)
                throw new ArgumentNullException(nameof(wallpost));

            _context.WallPosts.Add(wallpost);
        }

        public void Update(WallPost wallpost)
        {
            if (wallpost == null)
                throw new ArgumentNullException(nameof(wallpost));

            _context.Entry(wallpost).State = EntityState.Modified;
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

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}