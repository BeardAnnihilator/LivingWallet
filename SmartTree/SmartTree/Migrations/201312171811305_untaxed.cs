namespace SmartTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class untaxed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "untaxed", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "untaxed");
        }
    }
}
