namespace SmartTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsernameAsEmail : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "UserName");
            AddColumn("dbo.Users", "Email", c => c.String());
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
