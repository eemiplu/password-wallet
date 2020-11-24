using PasswordWallet.DbModels;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;

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

        public static string GetLocalIP()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address.ToString();
            }
        }
    }
}
