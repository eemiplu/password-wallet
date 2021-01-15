using PasswordWallet.Database.DbModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordWallet.DbModels
{
    [Table("Passwords")]
    public class Password
    {
        [Key]
        [Required]
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("PasswordHash", Order = 1)]
        public string PasswordHash { get; set; }

        [Required]
        [Column("IdUser", Order = 2)]
        [ForeignKey("User")]
        public int IdUser { get; set; }

        [Column("WebAddress", Order = 3)]
        public string WebAddress { get; set; }

        [Column("Description", Order = 4)]
        public string Description { get; set; }

        [Column("Login", Order = 5)]
        public string Login { get; set; }

        [Column("Deleted", Order = 6)]
        public bool Deleted { get; set; } = false;

        public User User { get; set; }
        public ICollection<SharedPassword> SharedPasswords { get; set; }
    }
}
