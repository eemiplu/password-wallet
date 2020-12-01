using PasswordWallet.Database.DbModels;
using PasswordWallet.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PasswordWallet.Views
{
    /// <summary>
    /// Interaction logic for LockedIpAddressesWindow.xaml
    /// </summary>
    public partial class LockedIpAddressesWindow : Window
    {
        ObservableCollection<IPAddress> IpAddresses;
        IpAddressManagement ipManagement = new IpAddressManagement();

        public LockedIpAddressesWindow()
        {
            InitializeComponent();

            IpAddresses = ipManagement.GetBlockedIps(Storage.GetUser().Id);

            this.listView.ItemsSource = IpAddresses;
        }

        private void UnlockIpAddressButton_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ip = (IPAddress)((Button)sender).DataContext;

            if (ipManagement.UnlockIP(ip) != null)
            {
                IpAddresses.Remove(ip);
            }          
        }
    }
}
