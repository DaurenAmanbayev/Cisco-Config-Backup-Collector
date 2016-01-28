using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Access
{
    class Init : DropCreateDatabaseIfModelChanges<RconfigContext>
    {
        protected override void Seed(RconfigContext context)
        {
            base.Seed(context);

            //var office806 = new Location { LocationName = "Office806" };
            //context.Attendees.Add(new Attendee
            //{
            //    DateAdded = DateTime.UtcNow,
            //    LastName = "Ivanov",
            //    Location = office806
            //});
            //context.Attendees.Add(new Attendee
            //{
            //    DateAdded = DateTime.UtcNow,
            //    LastName = "Petrov",
            //    Location = office806
            //});
            //context.SaveChanges();
        }
    }
}
