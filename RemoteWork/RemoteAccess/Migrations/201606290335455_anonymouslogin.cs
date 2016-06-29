namespace RemoteWork.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anonymouslogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "AnonymousLogin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "AnonymousLogin");
        }
    }
}
