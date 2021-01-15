using PasswordWallet.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Logic.HelperClasses
{
    public class RecordSerializer
    {
        private static char delimiter = '|';

        public string PasswordObjectToString(Password password)
        {
            return "" + password.Id + delimiter + password.PasswordHash + delimiter + password.IdUser + delimiter + 
                password.WebAddress + delimiter + password.Description + delimiter + password.Login + delimiter + password.Deleted;
        }

        public Password StringToPasswordObject(string record)
        {
            Password password = new Password();

            string[] subs = record.Split(delimiter);

            password.Id = int.Parse(subs[0]);
            password.PasswordHash = subs[1];
            password.IdUser = int.Parse(subs[2]);
            password.WebAddress = subs[3];
            password.Description = subs[4];
            password.Login = subs[5];
            password.Deleted = bool.Parse(subs[6]);

            return password;
        }
    }
}
