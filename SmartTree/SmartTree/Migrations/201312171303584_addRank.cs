namespace SmartTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Rank", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Rank");
        }
    }
}
