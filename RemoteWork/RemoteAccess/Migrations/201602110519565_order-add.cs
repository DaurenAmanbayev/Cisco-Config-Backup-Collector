namespace RemoteWork.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Commands", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Commands", "Order");
        }
    }
}
