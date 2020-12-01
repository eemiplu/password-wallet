using System.Net;
using System.Net.Sockets;

namespace PasswordWallet.Logic.HelperClasses
{
    public class IpManager
    {
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
