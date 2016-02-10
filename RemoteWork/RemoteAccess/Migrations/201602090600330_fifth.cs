namespace RemoteWork.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Favorites", "Protocol_Id", "dbo.Protocols");
            DropIndex("dbo.Favorites", new[] { "Protocol_Id" });
            AlterColumn("dbo.Favorites", "Protocol_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Protocols", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Favorites", "Protocol_Id");
            AddForeignKey("dbo.Favorites", "Protocol_Id", "dbo.Protocols", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorites", "Protocol_Id", "dbo.Protocols");
            DropIndex("dbo.Favorites", new[] { "Protocol_Id" });
            AlterColumn("dbo.Protocols", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Favorites", "Protocol_Id", c => c.Int());
            CreateIndex("dbo.Favorites", "Protocol_Id");
            AddForeignKey("dbo.Favorites", "Protocol_Id", "dbo.Protocols", "Id");
        }
    }
}
