namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefiguringKeysToOurTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "SenderId" });
            DropPrimaryKey("dbo.TopicPosts");
            AddColumn("dbo.TopicPosts", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.TopicPosts", "SenderId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.TopicPosts", "Id");
            CreateIndex("dbo.TopicPosts", "SenderId");
            AddForeignKey("dbo.TopicPosts", "SenderId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Posts", "SenderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "SenderId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.TopicPosts", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.TopicPosts", new[] { "SenderId" });
            DropPrimaryKey("dbo.TopicPosts");
            DropColumn("dbo.TopicPosts", "SenderId");
            DropColumn("dbo.TopicPosts", "Id");
            AddPrimaryKey("dbo.TopicPosts", new[] { "TopicId", "PostId" });
            CreateIndex("dbo.Posts", "SenderId");
            AddForeignKey("dbo.Posts", "SenderId", "dbo.AspNetUsers", "Id");
        }
    }
}
