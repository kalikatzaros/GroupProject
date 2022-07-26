﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using GroupProject.Models;
using System.Collections.ObjectModel;
using GroupProject.Configurations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get { return $"{Name} {LastName}"; }

        }
        public bool IsDeactivated { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
        public DateTime DateOfBirth { get; set; }
        public string Thumbnail { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<Message> Senders { get; set; }
        public ICollection<Message> Receivers { get; set; }
        public ICollection<WallPost> WallPosts  { get; set; }
        public ICollection<TopicPost> TopicPosts { get; set; }



        public ApplicationUser()
        {
            Followees = new Collection<Following>();
            Followers = new Collection<Following>();
            Senders = new Collection<Message>();
            Receivers = new Collection<Message>();
            WallPosts = new Collection<WallPost>();
            TopicPosts = new Collection<TopicPost>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicPost> TopicPosts { get; set; }

        public DbSet<WallPost> WallPosts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Following> Followings { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new TopicPostConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            base.OnModelCreating(modelBuilder);


        }

        
    }
    }

