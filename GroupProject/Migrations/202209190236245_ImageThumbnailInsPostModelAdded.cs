namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageThumbnailInsPostModelAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Thumbnail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Thumbnail");
        }
    }
}
