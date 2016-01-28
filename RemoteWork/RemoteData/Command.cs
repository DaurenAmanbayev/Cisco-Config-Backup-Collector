using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Data
{
    public class Command
    {
        public Command()
        {
            Categories = new HashSet<Category>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(256)]
        public string Name { get; set; }    
        [Required]
        public virtual ICollection<Category> Categories { get; set; }
    }
}
