namespace WhoChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMessageFromApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Messages", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Messages", "ApplicationUser_Id");
            AddForeignKey("dbo.Messages", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
