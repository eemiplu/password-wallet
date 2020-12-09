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
    [Table("SharedPasswords")]
    public class SharedPassword
    {
        public SharedPassword() { }

        public SharedPassword(int idUser, int idPassword) 
        {
            IdUser = idUser;
            IdPassword = idPassword;
        }

        [Key]
        [Required]
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("IdUser", Order = 1)]
        [ForeignKey("User")]
        public int IdUser { get; set; }

        [Required]
        [Column("IdPassword", Order = 2)]
        [ForeignKey("Password")]
        public int IdPassword { get; set; }

        public User User { get; set; }
        public Password Password { get; set; }
    }
}
