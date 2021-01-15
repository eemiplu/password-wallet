using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Logic.HelperClasses
{
    public class Enums
    {
        public enum ActionType : int
        {
            view = 1,
            create = 2,
            update = 3,
            delete = 4,
            recovery = 5
        }

        public enum TableName : int
        {
            Logins = 1,
            Users = 2,
            Passwords = 3,
            SharedPasswords = 4,
            IPAddresses = 5
        }

        public enum Function : int
        {
            ShowPassword = 1,
            UpdatePassword = 2,
            DeletePassword = 3,
            AddPassword = 4,
            SharePassword = 5
        }
    }
}
