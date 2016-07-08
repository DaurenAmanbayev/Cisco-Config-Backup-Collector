using RemoteWork.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace WebAppRemoteConfig.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
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
        //USER PROFILE MODEL
        public DbSet<UserProfile> UserProfiles { get; set; }
        
        //REMOTE WORK MODEL
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