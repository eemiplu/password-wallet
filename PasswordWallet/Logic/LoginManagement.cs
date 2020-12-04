using PasswordWallet.Controllers;
using PasswordWallet.Controllers.Interfaces;
using PasswordWallet.Cryptography;
using PasswordWallet.Database.DbModels;
using PasswordWallet.DbModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PasswordWallet.Logic
{
    public class LoginManagement
    {
        private static LoginsController _loginsController = new LoginsController();
        private static IpAddressController _ipAddressController = new IpAddressController();
        private static UsersController _usersController = new UsersController();

        //IipAddressController _ipAddressController = new IpAddressController();
        //IUsersController _usersController = new UsersController();

        //public LoginManagement()
        //{
        //    this._ipAddressController = new IpAddressController();
        //    this._usersController = new UsersController();
        //}

        //public LoginManagement(IipAddressController ipC, IUsersController iuC)
        //{
        //    this._ipAddressController = ipC;
        //    this._usersController = iuC;

        //}

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

            //if (!CheckIfAccountIsBlocked(login))
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

        public Login GetLastLogin(int userId, bool isSuccesful = true)
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
                lastLogin = findLastUserLogin(allUserLoginData, isSuccesful);
            }
            else
            {
                return null;
            }

            return lastLogin;
        }

        public Login findLastUserLogin(ObservableCollection<Login> userLoginData, bool isSuccesful)
        {
            var lastLogin = userLoginData.FirstOrDefault(u => u.Correct == isSuccesful);

            foreach (Login loginData in userLoginData)
            {
                if (loginData.Correct == isSuccesful && loginData.Time > lastLogin.Time)
                {
                    lastLogin = loginData;
                }
            }
            return lastLogin;
        }

        public bool CheckIfIPAddressIsBlocked(IPAddress iPAddress)
        {
            if (iPAddress.IncorrectLoginTrials >= 4)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfAccountIsBlocked(string login)
        {
            var user = _usersController.GetUser(login);
            var loginData = _loginsController.GetLastIncorrectLogin(user.Id);
            if (loginData == null)
                return false;

            if (user.IncorrectLogins >= 4) // && loginData.Time.AddMinutes(2) > DateTime.Now
            {
                return true;
            }
            return false;
        }

        public int UserVerificationTimeInSeconds(string ip, string login)
        {
            var iPAddress = Storage.IpAddress;
            var user = _usersController.GetUser(login);

            if (user == null)
                return 0;

            if (iPAddress.IncorrectLoginTrials >= 4)
                return -1;

            return Math.Max(getVerificationTime(user.IncorrectLogins), getVerificationTime(iPAddress.IncorrectLoginTrials));
        }

        public int getVerificationTime(int incorrectLoginTrials)
        {
            if (incorrectLoginTrials == 2)
                return 5;
            if (incorrectLoginTrials == 3)
                return 10;
            if (incorrectLoginTrials >= 4)
                return 120;

            return 0;
        }
    }
}
