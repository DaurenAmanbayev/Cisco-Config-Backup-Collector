using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using RemoteWork.Data;
using System.Data.Entity.Validation;

namespace RemoteWork.Access
{
    public class RconfigContext: DbContext
    {
        public RconfigContext() :
            base(@"data source=.\SQLEXPRESS;initial catalog=RemoteWorkDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
            //@"data source=(localdb)\v11.0;initial catalog=RemoteWorkDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Configurations.Add(new FavoriteConfig());//custom configuration
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<RemoteTask> RemoteTasks { get; set; }
        public DbSet<Protocol> Protocols { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Location> Locations { get; set; }    
    }
}
