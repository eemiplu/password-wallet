using PasswordWallet.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Database.DbModels
{
    [Table("Logins")]
    public class Login
    {
        [Key]
        [Required]
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("IdUser", Order = 1)]
        [ForeignKey("User")]
        public int IdUser { get; set; }

        [Required]
        [Column("Time", Order = 2)]
        public DateTime Time { get; set; }

        [Required]
        [Column("IdIpAddress", Order = 3)]
        [ForeignKey("IPAddress")]
        public int IdIpAddress { get; set; }

        [Required]
        [Column("Correct", Order = 4)]
        public bool Correct { get; set; }

        public User User { get; set; }
        public IPAddress IPAddress { get; set; }
    }
}
