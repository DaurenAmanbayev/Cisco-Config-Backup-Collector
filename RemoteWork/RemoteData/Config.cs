using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Data
{
    public class Config
    {
        [Key]
        public int Id { get; set; }
        [Required, Column("ConfigData",TypeName="ntext")]
        public string Current { get; set; }
        public DateTime? Date { get; set; }
    }
}
