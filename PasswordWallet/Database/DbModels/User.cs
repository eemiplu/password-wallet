using PasswordWallet.Database.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordWallet.DbModels
{
    [Table("Users")]
    public class User
    {
        public User() { }

        public User(String login, String passwordHash, String salt, bool isPasswordStoredAsHash)
        {
            Login = login;
            PasswordHash = passwordHash;
            Salt = salt;
            IsPasswordStoredAsHash = isPasswordStoredAsHash;
        }

        [Key]
        [Required]
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("Login", Order = 1)]
        public string Login { get; set; }

        [Column("Email", Order = 2)]
        public string Email { get; set; }

        [Required]
        [Column("PasswordHash", Order = 3)]
        public string PasswordHash { get; set; }

        [Column("Salt", Order = 4)]
        public string Salt { get; set; }

        [Column("IsPasswordStoredAsHash", Order = 5)]
        public bool IsPasswordStoredAsHash { get; set; }

        [Required]
        [Column("IncorrectLogins", Order = 6)]
        public int IncorrectLogins { get; set; } = 0;

        public ICollection<Password> Passwords { get; set; }
        public ICollection<Login> Logins { get; set; }
        public ICollection<SharedPassword> SharedPasswords { get; set; }
    }
}
