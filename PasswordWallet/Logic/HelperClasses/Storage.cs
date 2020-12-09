using PasswordWallet.Database.DbModels;
using PasswordWallet.DbModels;
using System.Collections.ObjectModel;

namespace PasswordWallet.Logic
{
    public static class Storage
    {
        private static User _user = null;

        public static IPAddress IpAddress;

        public static Mode applicationMode = Mode.Read;

        public static ObservableCollection<Password> StoredPasswordsList = new ObservableCollection<Password>();

        public static User CreateUser(User user)
        {
            if (_user == null)
            {
                _user = user;
            }

            return _user;
        }

        public static User GetUser()
        {
            return _user;
        }

        public static User DeleteUser()
        {
            return _user = null;
        }

        public enum Mode
        {
            Read,
            Write
        }

        //public static IPAddress SaveIpAddress(IPAddress ipAddress)
        //{
        //    if (_ipAddress == null)
        //    {
        //        _ipAddress = ipAddress;
        //    }

        //    return _ipAddress;
        //}

        //public static IPAddress GetIpAddress()
        //{
        //    return _ipAddress;
        //}

        //public static   IPAddress DeleteIpAddress()
        //{
        //    return _ipAddress = null;
        //}
    }
}
