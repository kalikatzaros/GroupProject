namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClearDummyData : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE  FROM Messages");
        }
        
        public override void Down()
        {
        }
    }
}
