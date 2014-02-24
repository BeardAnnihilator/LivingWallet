namespace SmartTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservedPartOfBalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ReservedPartOfBalance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ReservedPartOfBalance");
        }
    }
}
