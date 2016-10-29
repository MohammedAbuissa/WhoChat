namespace WhoChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MsgText = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        From_Id = c.String(maxLength: 128),
                        To_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.From_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.To_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.From_Id)
                .Index(t => t.To_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "To_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "From_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "To_Id" });
            DropIndex("dbo.Messages", new[] { "From_Id" });
            DropIndex("dbo.Messages", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Messages");
        }
    }
}
