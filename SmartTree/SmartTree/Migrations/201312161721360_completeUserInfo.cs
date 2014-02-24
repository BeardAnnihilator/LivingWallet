namespace SmartTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class completeUserInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "MultiCoins", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Balance", c => c.Double(nullable: false));
            AddColumn("dbo.Users", "Father_UserId", c => c.Int());
            AddForeignKey("dbo.Users", "Father_UserId", "dbo.Users", "UserId");
            CreateIndex("dbo.Users", "Father_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Father_UserId" });
            DropForeignKey("dbo.Users", "Father_UserId", "dbo.Users");
            DropColumn("dbo.Users", "Father_UserId");
            DropColumn("dbo.Users", "Balance");
            DropColumn("dbo.Users", "MultiCoins");
        }
    }
}
