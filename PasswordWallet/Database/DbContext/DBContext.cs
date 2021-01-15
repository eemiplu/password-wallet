using PasswordWallet.Database.DbModels;
using System.Data.Entity;

namespace PasswordWallet.DbModels
{
    public class DBContext : DbContext
    {
        public DBContext() : base("name=DBContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<SharedPassword> SharedPasswords { get; set; }
        public DbSet<IPAddress> IPAddresses { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<DataChange> DataChanges { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<TableName> TableNames { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<FunctionRun> FunctionRuns { get; set; }
    }
}
