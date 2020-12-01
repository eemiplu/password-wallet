using PasswordWallet.Controllers;
using PasswordWallet.Database.DbModels;
using System.Linq;

namespace PasswordWallet.Logic
{
    public class LoginManagement
    {
        private static LoginsController _loginsController = new LoginsController();

        public static Login GetLastLogin(int userId, bool isSuccesful = true)
        {
            Login lastLogin;

            var allUserLoginData = _loginsController.GetAllLoginsForUser(userId);

            if (allUserLoginData.Count() == 0)
            {
                return null;
            }

            lastLogin = allUserLoginData.FirstOrDefault(u => u.Correct == isSuccesful);

            if (lastLogin != null)
            {
                foreach (Login loginData in allUserLoginData)
                {
                    if (loginData.Correct == isSuccesful && loginData.Time > lastLogin.Time)
                    {
                        lastLogin = loginData;
                    }
                }
            }
            else
            {
                return null;
            }

            return lastLogin;
        }

        //public static Login AddNewLogin(Login login)
        //{


        //    login.IdUser = Storage.GetUser().Id;



        //    EncryptionManager encryptionManager = new EncryptionManager();
        //    password.PasswordHash = encryptionManager.AESEncrypt(Storage.GetUser().Salt, password.PasswordHash);

        //    password = _passwordsController.AddPassword(password);

        //    return login;
        //}

        //public static void DeletePassword(int id)
        //{
        //    _passwordsController.DeletePassword(id);
        //}

        //public static Password ChangePassword(Password password)
        //{
        //    EncryptionManager encryptionManager = new EncryptionManager();
        //    password.PasswordHash = encryptionManager.AESEncrypt(Storage.GetUser().Salt, password.PasswordHash);

        //    password = _passwordsController.UpdatePassword(password);

        //    return password;
        //}

        //public static ObservableCollection<Password> GetAllUserPasswords()
        //{
        //    return _passwordsController.GetAllPasswordsForUser(Storage.GetUser().Id);
        //}

        //public static void UpdateAllUserPasswords(String oldSalt, String newSalt)
        //{
        //    foreach (Password pass in Storage.StoredPasswordsList)
        //    {
        //        EncryptionManager encryptionManager = new EncryptionManager();
        //        string plainPassword = encryptionManager.AESDecrypt(oldSalt, pass.PasswordHash);

        //        pass.PasswordHash = encryptionManager.AESEncrypt(newSalt, plainPassword);

        //        _passwordsController.UpdatePassword(pass);
        //    }
        //}
    }
}
