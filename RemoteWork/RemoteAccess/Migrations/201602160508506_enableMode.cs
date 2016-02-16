namespace RemoteWork.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enableMode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Credentials", "Username", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Credentials", "Domain", c => c.String(maxLength: 255));
            AlterColumn("dbo.Credentials", "Password", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Credentials", "EnablePassword", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Credentials", "EnablePassword", c => c.String());
            AlterColumn("dbo.Credentials", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Credentials", "Domain", c => c.String());
            AlterColumn("dbo.Credentials", "Username", c => c.String(nullable: false));
        }
    }
}
