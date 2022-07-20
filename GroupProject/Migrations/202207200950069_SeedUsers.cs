namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [LastName]) VALUES (N'8233cbcf-4797-49fa-90fe-a51f1834f34a', N'admin@gmail.com', 0, N'AEvYfGdTaiBs/5Khdv1LsRNGrAR4E6t6Sp443yEXxFKLaoP/hHMxjKbaMWjYckuUOw==', N'511d5c62-c8b4-4ef1-97b6-ff4515a220ce', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com', NULL, NULL)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [LastName]) VALUES (N'937151ec-f443-4c7e-bcf0-7f555d914ebb', N'vidly@gmail.com', 0, N'AITM0VBR1Jh4SQscK9gPwEgzCxhrGpzr7+efWnvaOL+Tmt6IoyvuUGJqDj2hKXA3Cw==', N'5aecab81-b0ce-46f3-94fc-b5403069a00d', NULL, 0, 0, NULL, 1, 0, N'vidly@gmail.com', NULL, NULL)

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a08de290-9503-43a8-98a1-9dd0a82d1ad5', N'Admin')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8233cbcf-4797-49fa-90fe-a51f1834f34a', N'a08de290-9503-43a8-98a1-9dd0a82d1ad5')

");
        }
        
        public override void Down()
        {
        }
    }
}
