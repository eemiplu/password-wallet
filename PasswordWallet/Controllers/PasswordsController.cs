using PasswordWallet.Database.DbModels;
using PasswordWallet.DbModels;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;

namespace PasswordWallet.Controllers
{
    public class PasswordsController
    {
        private DBContext db = new DBContext();

        public Password AddPassword(Password password)
        {
            try
            {
                db.Passwords.Add(password);
                db.SaveChanges();

                return password;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Adding password to database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return null;
        }

        public Password DeletePassword(int id)
        {
            try
            {
                Password password = db.Passwords.FirstOrDefault(p => p.Id == id);
                password.Deleted = true;

                db.Entry(password).State = EntityState.Modified;
                db.SaveChanges();

                return password;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Deleting password from database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return null;
        }

        public Password UpdatePassword(Password password)
        {
            try
            {
                db.Set<Password>().AddOrUpdate(password);
                db.SaveChanges();

                return password;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Updating password in database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return null;
        }

        public Password GetPassword(int id)
        {
            Password password = db.Passwords.FirstOrDefault(p => p.Id == id);

            return password;
        }

        public ObservableCollection<Password> GetAllPasswordsForUser(int id)
        {
            ObservableCollection<Password> allUserPasswords = new ObservableCollection<Password>(db.Passwords.Where(p => p.IdUser == id && p.Deleted == false));

            foreach (var pass in db.SharedPasswords.Where(p => p.IdUser == id))
            {
                var password = db.Passwords.FirstOrDefault(p => p.Id == pass.IdPassword && p.Deleted == false);
                if (password != null)
                {
                    allUserPasswords.Add(password);
                }
            }

            return allUserPasswords;
        }
    }
}
