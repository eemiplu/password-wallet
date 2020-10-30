using PasswordWallet.DbModels;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

        public void DeletePassword(int id)
        {
            try
            {
                db.Passwords.Remove(db.Passwords.FirstOrDefault(p => p.Id == id));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Deleting password from database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public Password UpdatePassword(Password password)
        {
            try
            {
                db.Entry(password).State = EntityState.Modified;
                db.SaveChanges();

                return password;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Updating password in database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return null;
        }

        public ObservableCollection<Password> GetAllPasswordsForUser(int id)
        {
            return new ObservableCollection<Password>(db.Passwords.Where(p => p.IdUser == id));
        }
    }
}
