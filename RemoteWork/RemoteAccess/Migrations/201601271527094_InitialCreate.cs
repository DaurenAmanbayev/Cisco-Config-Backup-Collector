namespace RemoteWork.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commands",
                c => new
                    {
                        CommandId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RemoteTask_RemoteTaskId = c.Int(),
                    })
                .PrimaryKey(t => t.CommandId)
                .ForeignKey("dbo.RemoteTasks", t => t.RemoteTask_RemoteTaskId)
                .Index(t => t.RemoteTask_RemoteTaskId);
            
            CreateTable(
                "dbo.Configs",
                c => new
                    {
                        ConfigurationId = c.Int(nullable: false, identity: true),
                        Current = c.String(nullable: false),
                        Date = c.DateTime(),
                        Favorite_FavoriteId = c.Int(),
                    })
                .PrimaryKey(t => t.ConfigurationId)
                .ForeignKey("dbo.Favorites", t => t.Favorite_FavoriteId)
                .Index(t => t.Favorite_FavoriteId);
            
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        CredentialId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Domain = c.String(),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CredentialId);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        FavoriteId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Address = c.String(nullable: false, maxLength: 50),
                        Port = c.Int(nullable: false),
                        Protocol = c.String(),
                        Date = c.DateTime(),
                        Credential_CredentialId = c.Int(),
                        Group_GroupId = c.Int(),
                        RemoteTask_RemoteTaskId = c.Int(),
                    })
                .PrimaryKey(t => t.FavoriteId)
                .ForeignKey("dbo.Credentials", t => t.Credential_CredentialId)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId)
                .ForeignKey("dbo.RemoteTasks", t => t.RemoteTask_RemoteTaskId)
                .Index(t => t.Credential_CredentialId)
                .Index(t => t.Group_GroupId)
                .Index(t => t.RemoteTask_RemoteTaskId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.RemoteTasks",
                c => new
                    {
                        RemoteTaskId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 256),
                        Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.RemoteTaskId);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ReportId = c.Int(nullable: false, identity: true),
                        Status = c.Boolean(nullable: false),
                        Info = c.String(maxLength: 256),
                        Favorite_FavoriteId = c.Int(nullable: false),
                        Task_RemoteTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReportId)
                .ForeignKey("dbo.Favorites", t => t.Favorite_FavoriteId, cascadeDelete: true)
                .ForeignKey("dbo.RemoteTasks", t => t.Task_RemoteTaskId, cascadeDelete: true)
                .Index(t => t.Favorite_FavoriteId)
                .Index(t => t.Task_RemoteTaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "Task_RemoteTaskId", "dbo.RemoteTasks");
            DropForeignKey("dbo.Reports", "Favorite_FavoriteId", "dbo.Favorites");
            DropForeignKey("dbo.Favorites", "RemoteTask_RemoteTaskId", "dbo.RemoteTasks");
            DropForeignKey("dbo.Commands", "RemoteTask_RemoteTaskId", "dbo.RemoteTasks");
            DropForeignKey("dbo.Favorites", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.Favorites", "Credential_CredentialId", "dbo.Credentials");
            DropForeignKey("dbo.Configs", "Favorite_FavoriteId", "dbo.Favorites");
            DropIndex("dbo.Reports", new[] { "Task_RemoteTaskId" });
            DropIndex("dbo.Reports", new[] { "Favorite_FavoriteId" });
            DropIndex("dbo.Favorites", new[] { "RemoteTask_RemoteTaskId" });
            DropIndex("dbo.Favorites", new[] { "Group_GroupId" });
            DropIndex("dbo.Favorites", new[] { "Credential_CredentialId" });
            DropIndex("dbo.Configs", new[] { "Favorite_FavoriteId" });
            DropIndex("dbo.Commands", new[] { "RemoteTask_RemoteTaskId" });
            DropTable("dbo.Reports");
            DropTable("dbo.RemoteTasks");
            DropTable("dbo.Groups");
            DropTable("dbo.Favorites");
            DropTable("dbo.Credentials");
            DropTable("dbo.Configs");
            DropTable("dbo.Commands");
        }
    }
}
