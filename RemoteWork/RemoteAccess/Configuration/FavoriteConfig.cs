using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteWork.Data;
using System.Data.Entity.ModelConfiguration;

namespace RemoteWork.Configuration
{
    class FavoriteConfig : EntityTypeConfiguration<Favorite>
    {
        public FavoriteConfig()
        {
            //HasKey(p => p.AttendeeID);
            //Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            //Property(p => p.LastName).IsRequired().HasMaxLength(50);
        }

    }
}
