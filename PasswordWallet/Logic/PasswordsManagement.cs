using PasswordWallet.Controllers;
using PasswordWallet.Cryptography;
using PasswordWallet.Database.DbModels;
using PasswordWallet.DbModels;
using System;
using System.Collections.ObjectModel;

namespace PasswordWallet.Logic
{
    public class PasswordsManagement
    {
        private static PasswordsController _passwordsController = new PasswordsController();

        private static SharedPasswordsController _sharedPasswordsController = new SharedPasswordsController();

        private static UsersController _usersController = new UsersController();

        public static Password AddNewPassword(Password password)
        {
            password.IdUser = Storage.GetUser().Id;

            EncryptionManager encryptionManager = new EncryptionManager();
            password.PasswordHash = encryptionManager.AESEncrypt(Storage.GetUser().Salt, password.PasswordHash);

            password = _passwordsController.AddPassword(password);

            return password;
        }

        public static void DeletePassword(int id)
        {
            _passwordsController.DeletePassword(id);
        }

        public static Password ChangePassword(Password password)
        {
            EncryptionManager encryptionManager = new EncryptionManager();
            password.PasswordHash = encryptionManager.AESEncrypt(Storage.GetUser().Salt, password.PasswordHash);

            password = _passwordsController.UpdatePassword(password);

            return password;
        }

        public static ObservableCollection<Password> GetAllUserPasswords()
        {
            return _passwordsController.GetAllPasswordsForUser(Storage.GetUser().Id);
        }

        public static void UpdateAllUserPasswords(String oldSalt, String newSalt)
        {
            foreach (Password pass in Storage.StoredPasswordsList)
            {
                EncryptionManager encryptionManager = new EncryptionManager();
                string plainPassword = encryptionManager.AESDecrypt(oldSalt, pass.PasswordHash);

                pass.PasswordHash = encryptionManager.AESEncrypt(newSalt, plainPassword);

                _passwordsController.UpdatePassword(pass);
            }
        }

        public SharedPassword SharePassword(string login, int idPasword)
        {
            SharedPassword sharedPassword = null;

            if (_passwordsController.GetPassword(idPasword).IdUser.Equals(Storage.GetUser().Id))
            {
                User user = _usersController.GetUser(login);

                if (user != null)
                {
                    sharedPassword = _sharedPasswordsController.AddSharedPassword(new SharedPassword(user.Id, idPasword));
                }
                else
                {
                    sharedPassword = null;
                }
            }

            return sharedPassword;
        }
    }
}
