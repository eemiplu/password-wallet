using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordWallet.DbModels
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Required]
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("Login", Order = 1)]
        public string Login { get; set; }

        [Required]
        [Column("PasswordHash", Order = 2)]
        public string PasswordHash { get; set; }

        [Column("Salt", Order = 3)]
        public string Salt { get; set; }

        [Column("IsPasswordStoredAsHash", Order = 4)]
        public bool IsPasswordStoredAsHash { get; set; }

        public ICollection<Password> Passwords { get; set; }
    }
}
