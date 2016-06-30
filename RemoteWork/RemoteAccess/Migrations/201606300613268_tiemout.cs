namespace RemoteWork.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tiemout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Favorites", "TimeOut", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Favorites", "TimeOut");
        }
    }
}
