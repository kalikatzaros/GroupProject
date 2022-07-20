using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GroupProject.Models;

namespace GroupProject.Configurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            HasMany(u => u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            HasMany(u => u.Senders)
                .WithRequired(m => m.Receiver)
                .WillCascadeOnDelete(false);

            HasMany(u => u.Receivers)
                .WithRequired(m => m.Sender)
                .WillCascadeOnDelete(false);

            

        }
    }
}