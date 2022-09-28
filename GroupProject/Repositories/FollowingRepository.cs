using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GroupProject.Repositories
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Following> GetFollowings(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .ToList();
        }

        public List<string> GetFolloweesIds(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FolloweeId)
                .ToList();
        }

        public int GetFollowersCount(string userId)
        {
            return _context.Followings
                .Include(f=>f.Followee)
                .Where(f => f.FollowerId == userId&&f.Followee.IsDeactivated==false)
                .ToList().Count();
        }

        public int GetFolloweesCount(string userId)
        {
            return _context.Followings
                .Include(f => f.Follower)
                .Where(f => f.FolloweeId == userId && f.Follower.IsDeactivated == false)
                .ToList().Count();
        }
        public Following GetFollowing(string followerId, string followeeId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.FolloweeId == followeeId && f.FollowerId == followerId);
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}