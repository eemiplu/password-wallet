using PasswordWallet.Controllers;
using PasswordWallet.Cryptography;
using PasswordWallet.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Logic
{
    public static class UserManagement
    {
        private static UsersController _usersController = new UsersController();

        public static User RegisterNewUser(String login, String password, bool passwordShouldBeKeptAsHash)
        {
            User user = _usersController.GetUser(login);
            EncryptionManager encryptionManager = new EncryptionManager();
            String encryptedPassword = "";
            String salt;

            if (user != null)
            {
                return user;
            }

            salt = encryptionManager.GenerateSalt();
            encryptedPassword = encryptionManager.EncryptPassword(password, passwordShouldBeKeptAsHash, salt);

            user = new User(login, encryptedPassword, salt, passwordShouldBeKeptAsHash);

            _usersController.AddUser(user);

            return _usersController.GetUser(login);
        }

        public static User CheckLoginData(String login, String password)
        {
            User user = _usersController.GetUser(login);
            EncryptionManager encryptionManager = new EncryptionManager();
            String encryptedPassword = "";

            if (user == null)
            {
                return user;
            }

            encryptedPassword = encryptionManager.EncryptPassword(password, user.IsPasswordStoredAsHash, user.Salt);

            if (!user.PasswordHash.ToString().Equals(encryptedPassword))
            {
                user = null;
            }

            return user;
        }

        public static void ChangePassword(User user, String password)
        {
            //zabawa hasłem tak samo jak przy register i login + zmiana wszystkich haseł w portfelu i zmiana soli jeśli hash/nie hash
        }
    }
}
