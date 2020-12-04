using PasswordWallet.Database.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Controllers.Interfaces
{
    public interface IipAddressController
    {
        IPAddress GetIP(string ip);
        IPAddress UpdateIP(IPAddress ipAddress);
    }
}
