using PasswordWallet.Controllers;
using PasswordWallet.Cryptography;
using PasswordWallet.DbModels;
using System;

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

            user = _usersController.AddUser(user);

            return user;
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

        public static User ChangePassword(String password, bool passwordShouldBeKeptAsHash)
        {
            User user = _usersController.GetUser(Storage.GetUser().Login);
            String oldSalt = user.Salt;
            String newSalt;
            EncryptionManager encryptionManager = new EncryptionManager();
            String encryptedPassword = "";

            if (user == null)
            {
                return user;
            }

            newSalt = encryptionManager.GenerateSalt();
            encryptedPassword = encryptionManager.EncryptPassword(password, passwordShouldBeKeptAsHash, newSalt);

            user.PasswordHash = encryptedPassword;
            user.Salt = newSalt;
            user.IsPasswordStoredAsHash = passwordShouldBeKeptAsHash;

            user = _usersController.UpdateUser(user);

            Storage.DeleteUser();
            Storage.CreateUser(user);

            PasswordsManagement.UpdateAllUserPasswords(oldSalt, newSalt);

            return user;
        }
    }
}
