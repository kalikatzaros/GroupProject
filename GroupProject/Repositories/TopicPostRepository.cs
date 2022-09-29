using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GroupProject.Repositories
{
    public class TopicPostRepository
    {
        private readonly ApplicationDbContext _context;
        public TopicPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TopicPost> GetAll()
        {
            return _context.TopicPosts
                .Include(tp => tp.Post)
                .Include(tp => tp.Sender);
        }
        public TopicPost GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return _context.TopicPosts
                .Include(wp => wp.Post)
                .Include(wp => wp.Sender)
                .SingleOrDefault(wp => wp.Id == id);
        }

        public IEnumerable<TopicPost> GetByUser(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.TopicPosts
               .Include(tp => tp.Topic)
               .Include(tp => tp.Post)
               .Include(tp => tp.Sender)
               .Where(tp => tp.SenderId == id && tp.Sender.IsDeactivated == false)
               .OrderByDescending(tp => tp.Post.Datetime);
        }

        public void Create(TopicPost topicPost)
        {
            if (topicPost == null)
                throw new ArgumentNullException(nameof(topicPost));

            _context.TopicPosts.Add(topicPost);

            Save();
        }

        public void Update(TopicPost topicPost)
        {
            if (topicPost == null)
                throw new ArgumentNullException(nameof(topicPost));

            _context.Entry(topicPost).State = EntityState.Modified;
            Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

           TopicPost topicPost = GetById(id);

            if (topicPost == null)
                throw new Exception("Not found");

            _context.TopicPosts.Remove(topicPost);
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