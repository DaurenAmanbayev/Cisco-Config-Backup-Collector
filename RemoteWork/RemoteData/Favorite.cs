using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Data
{
    public class Favorite
    {
        public Favorite()
        {
            Configs = new HashSet<Config>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Hostname { get; set; }
        [Required, MaxLength(50)]
        public string Address { get; set; }        
        public int Port { get; set; }
        [Required]
        public virtual Protocol Protocol {get; set;}
        public virtual Location Location { get; set; }
        public DateTime? Date { get; set; }
        [Required]
        public virtual Credential Credential { get; set; }
        public virtual ICollection<Config> Configs { get; set; }
    }
}
