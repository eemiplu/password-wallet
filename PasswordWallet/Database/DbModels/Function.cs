using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Database.DbModels
{
    [Table("Functions")]
    public class Function
    {
        [Key]
        [Required]
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("FunctionName", Order = 1)]
        public string FunctionName { get; set; }

        [Column("Description", Order = 2)]
        public string Description { get; set; }

        public ICollection<FunctionRun> FunctionRuns { get; set; }
    }
}
