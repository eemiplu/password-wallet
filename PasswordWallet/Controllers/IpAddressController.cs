using PasswordWallet.Database.DbModels;
using PasswordWallet.DbModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordWallet.Controllers
{
    public class IpAddressController
    {
        private DBContext db = new DBContext();

        public IPAddress AddIP(string ip)
        {
            IPAddress ipAddress = new IPAddress(ip);

            try
            {
                db.IPAddresses.Add(ipAddress);
                db.SaveChanges();

                return ipAddress;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Adding login to database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return null;
        }

        public IPAddress UpdateIP(IPAddress ipAddress)
        {
            try
            {
                var foundIP = db.IPAddresses.FirstOrDefault(u => u.Id == ipAddress.Id);

                if (foundIP == null)
                {
                    return null;
                }

                db.Set<IPAddress>().AddOrUpdate(ipAddress);

                db.SaveChanges();

                return ipAddress;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Updating user failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return null;
        }

        public IPAddress GetIP(string ip)
        {
            var foundIP = db.IPAddresses.FirstOrDefault(u => u.IpAddress == ip);

            return foundIP;
        }

        public ObservableCollection<IPAddress> GetAllBlockedIPsForUser(int idUser)
        {
            ObservableCollection<IPAddress> iPAddresses = new ObservableCollection<IPAddress>();

            foreach (Login login in db.Logins.Where(l => l.IdUser == idUser))
            {
                IPAddress ipAddress = db.IPAddresses.Where(p => p.Id == login.IdIpAddress && p.IncorrectLoginTrials >= 4).FirstOrDefault();

                if (!iPAddresses.Contains(ipAddress) && ipAddress != null)
                {
                    iPAddresses.Add(ipAddress);
                }
            }

            return iPAddresses;
        }
    }
}
