using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.DataModels
{
    public class FunctionToDisplay
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string FunctionName { get; set; }
        public string Description { get; set; }
    }
}
