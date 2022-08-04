namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeFollowers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Followings (FollowerId,FolloweeId) VALUES ('1608bb82-fd50-430e-bf23-bde6ad46ffeb','309a7bd8-54ed-453d-8c0b-da20d51a58bc')");
            Sql("INSERT INTO Followings (FollowerId,FolloweeId) VALUES ('63a22ae3-46f7-42ff-87ca-71e24503bba2','1608bb82-fd50-430e-bf23-bde6ad46ffeb')");
        }
        
        public override void Down()
        {
        }
    }
}
