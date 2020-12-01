using PasswordWallet.Cryptography;
using PasswordWallet.DbModels;
using PasswordWallet.Logic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PasswordWallet.Views
{
    public partial class PasswordList : Window
    {
        public PasswordList()
        {
            InitializeComponent();

            LastSuccesfulLoginDateLabel.Content += LoginManagement.GetLastLogin(Storage.GetUser().Id, true) == null ? "-" : LoginManagement.GetLastLogin(Storage.GetUser().Id, true).Time.ToString();
            LastUnsuccesfulLoginDateLabel.Content += LoginManagement.GetLastLogin(Storage.GetUser().Id, false) == null ? "-" : LoginManagement.GetLastLogin(Storage.GetUser().Id, false).Time.ToString();

            Storage.StoredPasswordsList = PasswordsManagement.GetAllUserPasswords();

            this.listView.ItemsSource = Storage.StoredPasswordsList;

            AddPasswordButton.Click += AddPasswordButton_Click;
            EditMasterPasswordButton.Click += EditMasterPasswordButton_Click;
            LockedIPAddressesButton.Click += LockedIPAddressesButton_Click;
            LogOutButton.Click += LogOutButton_Click;
        }

        private void LockedIPAddressesButton_Click(object sender, RoutedEventArgs e)
        {
            LockedIpAddressesWindow window = new LockedIpAddressesWindow();
            window.Show();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Storage.DeleteUser();
            Storage.StoredPasswordsList = new ObservableCollection<Password>();

            LoginWindow window = new LoginWindow();
            window.Show();

            this.Close();
        }

        private void DecryptPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            Password password = (Password)((Button)sender).DataContext;

            EncryptionManager encryptionManager = new EncryptionManager();
            string plainPassword = encryptionManager.AESDecrypt(Storage.GetUser().Salt, password.PasswordHash);

            ShowPasswordWindow window = new ShowPasswordWindow(plainPassword);
            window.Show();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            Password password = (Password)((Button)sender).DataContext;

            EditPasswordWindow window = new EditPasswordWindow(password);
            window.Show();
        }

        private void DeletePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            Password password = (Password)((Button)sender).DataContext;

            Storage.StoredPasswordsList.Remove(password);

            PasswordsManagement.DeletePassword(password.Id);
        }

        private void AddPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            EditPasswordWindow window = new EditPasswordWindow();
            window.Show();
        }

        private void EditMasterPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserPasswordWindow window = new ChangeUserPasswordWindow();
            window.Show();
        }
    }
}
