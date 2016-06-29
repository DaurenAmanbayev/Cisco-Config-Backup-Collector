using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Data
{
    //возможно добавить поле проверка
    //при запуске тестового подключения, подтверждать, что устройство имеет правильные учетные данные
    public class Favorite
    {
        public Favorite()
        {
            Configs = new HashSet<Config>();
        }
        [Key]
        public int Id { get; set; }
        [Index("HostnameUniIndex", IsUnique = true)]
        [Required, MaxLength(100)]
        public string Hostname { get; set; }
        [Index("AddressUniIndex", IsUnique = true)]
        [Required, MaxLength(50)]
        public string Address { get; set; }        
        public int Port { get; set; }   
        [Required]
        public virtual Protocol Protocol {get; set;}     
        public virtual Location Location { get; set; }
        public DateTime? Date { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public virtual Credential Credential { get; set; }
        public virtual ICollection<RemoteTask> RemoteTasks { get; set; }
        public virtual ICollection<Config> Configs { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", Address);//base.ToString();
        }
    }
}
