using PasswordWallet.Controllers.Interfaces;
using PasswordWallet.DbModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace PasswordWallet.Controllers
{
    public class UsersController : IUsersController
    {
        private DBContext db = new DBContext();

        public User AddUser(User user)
        {
            try
            {
                var foundUser = db.Users.FirstOrDefault(u => u.Login == user.Login);

                if (foundUser == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    return user;
                }

                return null;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Adding user to database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
        }

        public User UpdateUser(User user)
        {
            try
            {
                var foundUser = db.Users.FirstOrDefault(u => u.Login == user.Login && u.Id == user.Id);

                if (foundUser == null)
                {
                    return null;
                }

                db.Entry(user).State = EntityState.Modified;

                db.SaveChanges();

                return user;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Updating user failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return null;
        }

        public User GetUser(String login)
        {
            var foundUser = db.Users.FirstOrDefault(u => u.Login == login);

            return foundUser; 
        }
    }
}
