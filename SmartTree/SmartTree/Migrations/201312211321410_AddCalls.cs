namespace SmartTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCalls : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ReceivedCalls", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "EmitedCalls", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "EmitedCalls");
            DropColumn("dbo.Users", "ReceivedCalls");
        }
    }
}
