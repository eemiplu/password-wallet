using PasswordWallet.DbModels;
using System.Collections.ObjectModel;

namespace PasswordWallet.Logic
{
    public static class Storage
    {
        private static User _user;

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
    }
}
