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
    [Table("FunctionRuns")]
    public class FunctionRun
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
        [Column("IdFunction", Order = 3)]
        [ForeignKey("Function")]
        public int IdFunction { get; set; }

        public User User { get; set; }
        public Function Function { get; set; }
    }
}
