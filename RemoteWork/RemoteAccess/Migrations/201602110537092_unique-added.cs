namespace RemoteWork.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueadded : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Favorites", "Hostname", unique: true, name: "HostnameUniIndex");
            CreateIndex("dbo.Favorites", "Address", unique: true, name: "AddressUniIndex");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Favorites", "AddressUniIndex");
            DropIndex("dbo.Favorites", "HostnameUniIndex");
        }
    }
}
