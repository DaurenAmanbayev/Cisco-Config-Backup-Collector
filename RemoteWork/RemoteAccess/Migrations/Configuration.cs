namespace RemoteWork.Access.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using RemoteWork.Data;
    using System.Collections.Generic;

    public  sealed class Configuration : DbMigrationsConfiguration<RemoteWork.Access.RconfigContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "RemoteWork.Access.RconfigContext";            
            AutomaticMigrationDataLossAllowed = true; 
        }

        //protected override void Seed(RemoteWork.Access.RconfigContext context)
        //{            

        //    //  This method will be called after migrating to the latest version.

        //    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //    //  to avoid creating duplicate seed data. E.g.
        //    //
        //    //    context.People.AddOrUpdate(
        //    //      p => p.FullName,
        //    //      new Person { FullName = "Andrew Peters" },
        //    //      new Person { FullName = "Brice Lambson" },
        //    //      new Person { FullName = "Rowan Miller" }
        //    //    );
        //    //
        //}
    }
}
