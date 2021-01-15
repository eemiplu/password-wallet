using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Database.DbModels
{
    [Table("TableNames")]
    public class TableName
    {
        [Key]
        [Required]
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("TableName", Order = 1)]
        public string Table_Name { get; set; }

        [Column("Description", Order = 2)]
        public string Description { get; set; }

        public ICollection<DataChange> DataChanges { get; set; }
    }
}
