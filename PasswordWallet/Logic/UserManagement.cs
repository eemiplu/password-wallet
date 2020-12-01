using PasswordWallet.Controllers;
using PasswordWallet.Cryptography;
using PasswordWallet.Database.DbModels;
using PasswordWallet.DbModels;
using PasswordWallet.Logic.HelperClasses;
using System;

namespace PasswordWallet.Logic
{
    public class UserManagement
    {
        private UsersController _usersController = new UsersController();
        private LoginsController _loginsController = new LoginsController();
        private IpAddressController _ipAddressController = new IpAddressController();

        public User RegisterNewUser(String login, String password, bool passwordShouldBeKeptAsHash)
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

        public User Login(String login, String password)
        {
            User user = _usersController.GetUser(login);
            EncryptionManager encryptionManager = new EncryptionManager();
            String encryptedPassword;

            if (user == null)
            {
                return user;
            }

            Login loginData = new Login(user.Id, DateTime.Now, Storage.IpAddress.Id);

            encryptedPassword = encryptionManager.EncryptPassword(password, user.IsPasswordStoredAsHash, user.Salt);

            if (!user.PasswordHash.ToString().Equals(encryptedPassword))
            {
                incrementIncorrectLoginTrials(user);

                loginData.Correct = false;
                user = null;
            }
            else
            {
                resetIncorrectLoginTrialsNumber(user);
            }

            _loginsController.AddLogin(loginData);

            return user;
        }

        private void incrementIncorrectLoginTrials(User user)
        {
            user.IncorrectLogins += 1;
            _usersController.UpdateUser(user);
            Storage.IpAddress.IncorrectLoginTrials += 1;
            _ipAddressController.UpdateIP(Storage.IpAddress);
        }

        private void resetIncorrectLoginTrialsNumber(User user)
        {
            user.IncorrectLogins = 0;
            _usersController.UpdateUser(user);
            Storage.IpAddress.IncorrectLoginTrials = 0;
            _ipAddressController.UpdateIP(Storage.IpAddress);
        }

        public User ChangePassword(String password, bool passwordShouldBeKeptAsHash)
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
