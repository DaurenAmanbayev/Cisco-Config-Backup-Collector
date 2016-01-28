using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Data
{
    public class RemoteTask
    {
        public RemoteTask()
        {
            Commands = new HashSet<Command>();
            Favorites = new HashSet<Favorite>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        [Required]
        public virtual ICollection<Command> Commands { get; set; }
        [Required]
        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}
