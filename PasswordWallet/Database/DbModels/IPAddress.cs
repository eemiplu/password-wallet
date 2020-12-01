using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Database.DbModels
{
    [Table("IPAddresses")]
    public class IPAddress
    {
        public IPAddress() { }

        public IPAddress(string ipAddress)
        {
            IpAddress = ipAddress;
        }

        [Key]
        [Required]
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("IpAddress", Order = 1)]
        public string IpAddress { get; set; }

        [Required]
        [Column("IncorrectLoginTrials", Order = 2)]
        public int IncorrectLoginTrials { get; set; } = 0;

        public ICollection<Login> Logins { get; set; }
    }
}
