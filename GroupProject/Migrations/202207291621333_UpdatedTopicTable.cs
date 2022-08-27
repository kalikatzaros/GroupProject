namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTopicTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Topics", "UserId");
            AddForeignKey("dbo.Topics", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Topics", new[] { "UserId" });
            DropColumn("dbo.Topics", "UserId");
        }
    }
}
