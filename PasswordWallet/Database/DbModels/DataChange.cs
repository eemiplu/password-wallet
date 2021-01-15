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
    [Table("DataChanges")]
    public class DataChange
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
        [Column("IdModifiedRecord", Order = 2)]
        public int IdModifiedRecord { get; set; }

        [Column("PreviousValueOfRecord", Order = 3)]
        public string PreviousValueOfRecord { get; set; }

        [Column("PresentValueOfRecord", Order = 4)]
        public string PresentValueOfRecord { get; set; }

        [Required]
        [Column("Time", Order = 5)]
        public DateTime Time { get; set; }

        [Required]
        [Column("IdActionType", Order = 6)]
        [ForeignKey("ActionType")]
        public int IdActionType { get; set; }

        [Required]
        [Column("IdTableName", Order = 7)]
        [ForeignKey("TableName")]
        public int IdTableName { get; set; }

        public User User { get; set; }
        public ActionType ActionType { get; set; }
        public TableName TableName { get; set; }
    }
}
