namespace SmartTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixParentChild : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Father_UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Father_UserId" });
            AddColumn("dbo.Users", "FatherId", c => c.Int());
            AddForeignKey("dbo.Users", "FatherId", "dbo.Users", "UserId");
            CreateIndex("dbo.Users", "FatherId");
            DropColumn("dbo.Users", "Father_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Father_UserId", c => c.Int());
            DropIndex("dbo.Users", new[] { "FatherId" });
            DropForeignKey("dbo.Users", "FatherId", "dbo.Users");
            DropColumn("dbo.Users", "FatherId");
            CreateIndex("dbo.Users", "Father_UserId");
            AddForeignKey("dbo.Users", "Father_UserId", "dbo.Users", "UserId");
        }
    }
}
