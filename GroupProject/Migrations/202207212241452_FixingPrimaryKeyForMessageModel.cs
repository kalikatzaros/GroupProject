namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingPrimaryKeyForMessageModel : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Messages");
            AddColumn("dbo.Messages", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Messages", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Messages");
            DropColumn("dbo.Messages", "Id");
            AddPrimaryKey("dbo.Messages", new[] { "SenderId", "ReceiverId" });
        }
    }
}
