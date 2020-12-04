using PasswordWallet.Controllers.Interfaces;
using PasswordWallet.Database.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordWallet.UnitTests.Adapters
{
    public class IpControllerAdapter : IipAddressController
    {
        public IPAddress GetIP(string ip)
        {
            return new IPAddress("192.168.2.3");
        }

        public IPAddress UpdateIP(IPAddress ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
