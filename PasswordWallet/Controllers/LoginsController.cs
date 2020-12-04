using PasswordWallet.Database.DbModels;
using PasswordWallet.DbModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordWallet.Controllers
{
    public class LoginsController
    {
        private DBContext db = new DBContext();

        public Login AddLogin(Login login)
        {
            try
            {
                db.Logins.Add(login);
                db.SaveChanges();

                return login;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Adding login to database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return null;
        }

        //public void Delete(int id)
        //{
        //    try
        //    {
        //        db.Passwords.Remove(db.Passwords.FirstOrDefault(p => p.Id == id));
        //        db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Exception occured: " + ex.Message, "Deleting password from database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //}

        //public Password UpdatePassword(Password password)
        //{
        //    try
        //    {
        //        db.Entry(password).State = EntityState.Modified;
        //        db.SaveChanges();

        //        return password;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Exception occured: " + ex.Message, "Updating password in database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }

        //    return null;
        //}

        public ObservableCollection<Login> GetAllLoginsForUser(int userId)
        {
            return new ObservableCollection<Login>(db.Logins.Where(p => p.IdUser == userId));
        }

        public ObservableCollection<Login> GetAllLoginsForIP(int ipAddressId)
        {
            return new ObservableCollection<Login>(db.Logins.Where(p => p.IdIpAddress == ipAddressId));
        }

        public Login GetLastIncorrectLogin(int userId)
        {
            var time = db.Logins.Where(p => p.IdUser == userId && p.Correct == false).Select(p => p.Time).DefaultIfEmpty();//.Max(x => x.Time);
            if (time == null)
                return null;
            return db.Logins.Where(p => p.IdUser == userId && p.Correct == false && p.Time == time.Max()).FirstOrDefault();
        }
    }
}
