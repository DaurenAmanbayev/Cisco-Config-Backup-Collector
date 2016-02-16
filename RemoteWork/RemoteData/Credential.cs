using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork.Data
{
    public class Credential
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CredentialName { get; set; }
        [Required, MaxLength(255)]
        public string Username { get; set; }
        [MaxLength(255)]
        public string Domain { get; set; }
        [Required, MaxLength(255)]
        public string Password { get; set; }
        [MaxLength(255)]
        public string EnablePassword { get; set; }

    }
}
