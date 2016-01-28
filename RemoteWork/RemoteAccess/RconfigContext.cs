using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using RemoteWork.Data;

namespace RemoteWork.Access
{
    public class RconfigContext: DbContext
    {
        public RconfigContext() :
            base(@"data source=(localdb)\v11.0;initial catalog=RemoteWorkDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
 
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Configurations.Add(new FavoriteConfig());//custom configuration
        }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<RemoteTask> RemoteTasks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Command> Commands { get; set; }
    
    }
}
