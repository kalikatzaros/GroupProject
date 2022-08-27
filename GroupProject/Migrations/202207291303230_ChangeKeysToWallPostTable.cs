namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeKeysToWallPostTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WallPosts", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WallPosts", new[] { "UserId" });
            DropPrimaryKey("dbo.WallPosts");
            AddColumn("dbo.WallPosts", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.WallPosts", "UserId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.WallPosts", "Id");
            CreateIndex("dbo.WallPosts", "UserId");
            AddForeignKey("dbo.WallPosts", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WallPosts", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WallPosts", new[] { "UserId" });
            DropPrimaryKey("dbo.WallPosts");
            AlterColumn("dbo.WallPosts", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.WallPosts", "Id");
            AddPrimaryKey("dbo.WallPosts", new[] { "UserId", "PostId" });
            CreateIndex("dbo.WallPosts", "UserId");
            AddForeignKey("dbo.WallPosts", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
