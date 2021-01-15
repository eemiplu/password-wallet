using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Database.DbModels
{
    [Table("ActionTypes")]
    public class ActionType
    {
        [Key]
        [Required]
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("Title", Order = 1)]
        public string Title { get; set; }

        [Column("Description", Order = 2)]
        public string Description { get; set; }

        public ICollection<DataChange> DataChanges { get; set; }
    }
}
