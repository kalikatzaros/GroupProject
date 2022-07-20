using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace GroupProject.Configurations
{
    public class TopicPostConfiguration : EntityTypeConfiguration<TopicPost>
    {
        public TopicPostConfiguration()
        {
            //HasKey(t => new { t.PostId, t.TopicId });
            ////HasMany(p => p.Post)
            //// .WithMany(t => t.Topic);
            
        }
    }
}