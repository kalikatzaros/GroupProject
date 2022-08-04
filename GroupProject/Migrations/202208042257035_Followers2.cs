namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Followers2 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Followings (FollowerId,FolloweeId) VALUES ('2b032c82-899d-436f-8e60-b0d8e54f15e8','3e7914b0-2158-4fad-9599-e6a065640915')");
            Sql("INSERT INTO Followings (FollowerId,FolloweeId) VALUES ('e3e69f7b-0777-48a6-bd21-76fa78d27dbc','2b032c82-899d-436f-8e60-b0d8e54f15e8')");
        }
        
        public override void Down()
        {
        }
    }
}
