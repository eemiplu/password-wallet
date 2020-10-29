using PasswordWallet.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordWallet.Controllers
{
    public class UsersController
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

                    return db.Users.First(u => u.Login == user.Login);
                }

                return null;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Adding user to database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                var foundUser = db.Users.FirstOrDefault(u => u.Login == user.Login && u.Id == user.Id);

                if (foundUser == null)
                {
                    //return null;  //TODO - co zwracać z funkcji
                }

                db.Entry(user).State = EntityState.Modified;

                db.SaveChanges();

                //return - TODO
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Updating user failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                //return null;
            }
        }

        public User GetUser(String login)
        {
            var foundUser = db.Users.FirstOrDefault(u => u.Login == login);

            //foundUser = foundUser != null ? foundUser : new User();

            return foundUser; 
        }
    }
}
