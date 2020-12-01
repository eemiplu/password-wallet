using PasswordWallet.Controllers;
using PasswordWallet.Database.DbModels;
using System.Collections.ObjectModel;

namespace PasswordWallet.Logic
{
    public class IpAddressManagement
    {
        private IpAddressController _IpAddressController = new IpAddressController();

        public IPAddress AddNewIp(string ip)
        {
            IPAddress ipAddress = _IpAddressController.GetIP(ip);

            if (ipAddress == null)
            {
                ipAddress = _IpAddressController.AddIP(ip);
            }

            return ipAddress;
        }

        public ObservableCollection<IPAddress> GetBlockedIps(int userId)
        {
            return _IpAddressController.GetAllBlockedIPsForUser(userId);
        }

        public IPAddress UnlockIP(IPAddress iPAddress)
        {
            iPAddress.IncorrectLoginTrials = 0;

            return _IpAddressController.UpdateIP(iPAddress);
        }
    }
}
