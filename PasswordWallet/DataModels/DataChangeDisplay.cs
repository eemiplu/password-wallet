using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.DataModels
{
    public class DataChangeDisplay
    {
        public int Id { get; set; }
        public int IdModifiedRecord { get; set; }
        public DateTime Time { get; set; }
        public string PreviousValueOfRecord { get; set; }
        public string PresentValueOfRecord { get; set; }
        public string ActionType { get; set; }
        public string TableName { get; set; }
    }
}
