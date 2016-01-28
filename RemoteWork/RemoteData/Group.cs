using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Data
{
    public class Group
    {
        public Group()
        {
            Favorites = new HashSet<Favorite>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(256)]
        public string Name { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}
