using PasswordWallet.Database.DbModels;
using PasswordWallet.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordWallet.Controllers
{
    class SharedPasswordsController
    {
        private DBContext db = new DBContext();

        public SharedPassword AddSharedPassword(SharedPassword sharedPassword)
        {
            try
            {
                if (db.SharedPasswords.FirstOrDefault(s => s.IdPassword == sharedPassword.IdPassword && s.IdUser == sharedPassword.IdUser) == null)
                { 
                    db.SharedPasswords.Add(sharedPassword);
                    db.SaveChanges();

                    return sharedPassword;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Sharing password failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return null;
        }
    }
}
