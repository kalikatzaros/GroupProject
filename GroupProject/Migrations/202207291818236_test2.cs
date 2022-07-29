namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TopicPosts", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.TopicPosts", new[] { "SenderId" });
            AlterColumn("dbo.TopicPosts", "SenderId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TopicPosts", "SenderId");
            AddForeignKey("dbo.TopicPosts", "SenderId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicPosts", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.TopicPosts", new[] { "SenderId" });
            AlterColumn("dbo.TopicPosts", "SenderId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TopicPosts", "SenderId");
            AddForeignKey("dbo.TopicPosts", "SenderId", "dbo.AspNetUsers", "Id");
        }
    }
}
