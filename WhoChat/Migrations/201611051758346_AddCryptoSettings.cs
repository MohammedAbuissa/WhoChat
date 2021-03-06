namespace WhoChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCryptoSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CryptoSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.Binary(),
                        IV = c.Binary(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CryptoSettings", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CryptoSettings", new[] { "User_Id" });
            DropTable("dbo.CryptoSettings");
        }
    }
}
