using System.Data.Entity;

namespace PasswordWallet.DbModels
{
    public class DBContext : DbContext
    {
        public DBContext() : base("name=DBContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
    }
}
