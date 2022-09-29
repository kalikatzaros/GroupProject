using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GroupProject.Repositories
{
    public class TopicRepository
    {
        private readonly ApplicationDbContext _context;
        public TopicRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Topic> GetAll()
        {
            return _context.Topics
                .Include(t => t.User)
                .OrderByDescending(t => t.Created);
        }
        public Topic GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return _context.Topics
                .Include(t => t.User)
                .SingleOrDefault(t => t.Id == id);
        }

        public void Create(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            _context.Topics.Add(topic);

            Save();
        }

        public void Update(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            _context.Entry(topic).State = EntityState.Modified;
            Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Topic topic = GetById(id);

            if (topic == null)
                throw new Exception("Not found");

            _context.Topics.Remove(topic);
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