using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Data
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual RemoteTask Task { get; set; }
        [Required]
        public virtual Favorite Favorite { get; set; }
        [Required]
        public bool Status { get; set; }
        [MaxLength(256)]
        public string Info { get; set; }
        [Column("Created", TypeName="datetime2")]
        public DateTime? Date { get; set; }
    }
}
