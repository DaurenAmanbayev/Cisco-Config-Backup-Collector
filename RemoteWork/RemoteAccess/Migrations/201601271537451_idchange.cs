namespace RemoteWork.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idchange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Favorites", "Credential_CredentialId", "dbo.Credentials");
            DropForeignKey("dbo.Configs", "Favorite_FavoriteId", "dbo.Favorites");
            DropForeignKey("dbo.Reports", "Favorite_FavoriteId", "dbo.Favorites");
            DropForeignKey("dbo.Favorites", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.Commands", "RemoteTask_RemoteTaskId", "dbo.RemoteTasks");
            DropForeignKey("dbo.Favorites", "RemoteTask_RemoteTaskId", "dbo.RemoteTasks");
            DropForeignKey("dbo.Reports", "Task_RemoteTaskId", "dbo.RemoteTasks");
            RenameColumn(table: "dbo.Configs", name: "Favorite_FavoriteId", newName: "Favorite_Id");
            RenameColumn(table: "dbo.Favorites", name: "Credential_CredentialId", newName: "Credential_Id");
            RenameColumn(table: "dbo.Favorites", name: "Group_GroupId", newName: "Group_Id");
            RenameColumn(table: "dbo.Commands", name: "RemoteTask_RemoteTaskId", newName: "RemoteTask_Id");
            RenameColumn(table: "dbo.Favorites", name: "RemoteTask_RemoteTaskId", newName: "RemoteTask_Id");
            RenameColumn(table: "dbo.Reports", name: "Favorite_FavoriteId", newName: "Favorite_Id");
            RenameColumn(table: "dbo.Reports", name: "Task_RemoteTaskId", newName: "RemoteTask_Id");
            RenameIndex(table: "dbo.Commands", name: "IX_RemoteTask_RemoteTaskId", newName: "IX_RemoteTask_Id");
            RenameIndex(table: "dbo.Configs", name: "IX_Favorite_FavoriteId", newName: "IX_Favorite_Id");
            RenameIndex(table: "dbo.Favorites", name: "IX_Credential_CredentialId", newName: "IX_Credential_Id");
            RenameIndex(table: "dbo.Favorites", name: "IX_Group_GroupId", newName: "IX_Group_Id");
            RenameIndex(table: "dbo.Favorites", name: "IX_RemoteTask_RemoteTaskId", newName: "IX_RemoteTask_Id");
            RenameIndex(table: "dbo.Reports", name: "IX_Favorite_FavoriteId", newName: "IX_Favorite_Id");
            RenameIndex(table: "dbo.Reports", name: "IX_Task_RemoteTaskId", newName: "IX_RemoteTask_Id");
            DropPrimaryKey("dbo.Commands");
            DropPrimaryKey("dbo.Configs");
            DropPrimaryKey("dbo.Credentials");
            DropPrimaryKey("dbo.Favorites");
            DropPrimaryKey("dbo.Groups");
            DropPrimaryKey("dbo.RemoteTasks");
            DropPrimaryKey("dbo.Reports");
            AddColumn("dbo.Commands", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Configs", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Credentials", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Favorites", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Groups", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.RemoteTasks", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Reports", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Commands", "Id");
            AddPrimaryKey("dbo.Configs", "Id");
            AddPrimaryKey("dbo.Credentials", "Id");
            AddPrimaryKey("dbo.Favorites", "Id");
            AddPrimaryKey("dbo.Groups", "Id");
            AddPrimaryKey("dbo.RemoteTasks", "Id");
            AddPrimaryKey("dbo.Reports", "Id");
            AddForeignKey("dbo.Favorites", "Credential_Id", "dbo.Credentials", "Id");
            AddForeignKey("dbo.Configs", "Favorite_Id", "dbo.Favorites", "Id");
            AddForeignKey("dbo.Reports", "Favorite_Id", "dbo.Favorites", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Favorites", "Group_Id", "dbo.Groups", "Id");
            AddForeignKey("dbo.Commands", "RemoteTask_Id", "dbo.RemoteTasks", "Id");
            AddForeignKey("dbo.Favorites", "RemoteTask_Id", "dbo.RemoteTasks", "Id");
            AddForeignKey("dbo.Reports", "RemoteTask_Id", "dbo.RemoteTasks", "Id", cascadeDelete: true);
            DropColumn("dbo.Commands", "CommandId");
            DropColumn("dbo.Configs", "ConfigurationId");
            DropColumn("dbo.Credentials", "CredentialId");
            DropColumn("dbo.Favorites", "FavoriteId");
            DropColumn("dbo.Groups", "GroupId");
            DropColumn("dbo.RemoteTasks", "RemoteTaskId");
            DropColumn("dbo.Reports", "ReportId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "ReportId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.RemoteTasks", "RemoteTaskId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Groups", "GroupId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Favorites", "FavoriteId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Credentials", "CredentialId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Configs", "ConfigurationId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Commands", "CommandId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Reports", "RemoteTask_Id", "dbo.RemoteTasks");
            DropForeignKey("dbo.Favorites", "RemoteTask_Id", "dbo.RemoteTasks");
            DropForeignKey("dbo.Commands", "RemoteTask_Id", "dbo.RemoteTasks");
            DropForeignKey("dbo.Favorites", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Reports", "Favorite_Id", "dbo.Favorites");
            DropForeignKey("dbo.Configs", "Favorite_Id", "dbo.Favorites");
            DropForeignKey("dbo.Favorites", "Credential_Id", "dbo.Credentials");
            DropPrimaryKey("dbo.Reports");
            DropPrimaryKey("dbo.RemoteTasks");
            DropPrimaryKey("dbo.Groups");
            DropPrimaryKey("dbo.Favorites");
            DropPrimaryKey("dbo.Credentials");
            DropPrimaryKey("dbo.Configs");
            DropPrimaryKey("dbo.Commands");
            DropColumn("dbo.Reports", "Id");
            DropColumn("dbo.RemoteTasks", "Id");
            DropColumn("dbo.Groups", "Id");
            DropColumn("dbo.Favorites", "Id");
            DropColumn("dbo.Credentials", "Id");
            DropColumn("dbo.Configs", "Id");
            DropColumn("dbo.Commands", "Id");
            AddPrimaryKey("dbo.Reports", "ReportId");
            AddPrimaryKey("dbo.RemoteTasks", "RemoteTaskId");
            AddPrimaryKey("dbo.Groups", "GroupId");
            AddPrimaryKey("dbo.Favorites", "FavoriteId");
            AddPrimaryKey("dbo.Credentials", "CredentialId");
            AddPrimaryKey("dbo.Configs", "ConfigurationId");
            AddPrimaryKey("dbo.Commands", "CommandId");
            RenameIndex(table: "dbo.Reports", name: "IX_RemoteTask_Id", newName: "IX_Task_RemoteTaskId");
            RenameIndex(table: "dbo.Reports", name: "IX_Favorite_Id", newName: "IX_Favorite_FavoriteId");
            RenameIndex(table: "dbo.Favorites", name: "IX_RemoteTask_Id", newName: "IX_RemoteTask_RemoteTaskId");
            RenameIndex(table: "dbo.Favorites", name: "IX_Group_Id", newName: "IX_Group_GroupId");
            RenameIndex(table: "dbo.Favorites", name: "IX_Credential_Id", newName: "IX_Credential_CredentialId");
            RenameIndex(table: "dbo.Configs", name: "IX_Favorite_Id", newName: "IX_Favorite_FavoriteId");
            RenameIndex(table: "dbo.Commands", name: "IX_RemoteTask_Id", newName: "IX_RemoteTask_RemoteTaskId");
            RenameColumn(table: "dbo.Reports", name: "RemoteTask_Id", newName: "Task_RemoteTaskId");
            RenameColumn(table: "dbo.Reports", name: "Favorite_Id", newName: "Favorite_FavoriteId");
            RenameColumn(table: "dbo.Favorites", name: "RemoteTask_Id", newName: "RemoteTask_RemoteTaskId");
            RenameColumn(table: "dbo.Commands", name: "RemoteTask_Id", newName: "RemoteTask_RemoteTaskId");
            RenameColumn(table: "dbo.Favorites", name: "Group_Id", newName: "Group_GroupId");
            RenameColumn(table: "dbo.Favorites", name: "Credential_Id", newName: "Credential_CredentialId");
            RenameColumn(table: "dbo.Configs", name: "Favorite_Id", newName: "Favorite_FavoriteId");
            AddForeignKey("dbo.Reports", "Task_RemoteTaskId", "dbo.RemoteTasks", "RemoteTaskId", cascadeDelete: true);
            AddForeignKey("dbo.Favorites", "RemoteTask_RemoteTaskId", "dbo.RemoteTasks", "RemoteTaskId");
            AddForeignKey("dbo.Commands", "RemoteTask_RemoteTaskId", "dbo.RemoteTasks", "RemoteTaskId");
            AddForeignKey("dbo.Favorites", "Group_GroupId", "dbo.Groups", "GroupId");
            AddForeignKey("dbo.Reports", "Favorite_FavoriteId", "dbo.Favorites", "FavoriteId", cascadeDelete: true);
            AddForeignKey("dbo.Configs", "Favorite_FavoriteId", "dbo.Favorites", "FavoriteId");
            AddForeignKey("dbo.Favorites", "Credential_CredentialId", "dbo.Credentials", "CredentialId");
        }
    }
}
