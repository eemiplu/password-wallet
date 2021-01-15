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
        LoginManagement loginManagement = new LoginManagement();

        public PasswordList()
        {
            InitializeComponent();

            LastSuccesfulLoginDateLabel.Content += loginManagement.GetLastLogin(Storage.GetUser().Id, true) == null ? "-" : loginManagement.GetLastLogin(Storage.GetUser().Id, true).Time.ToString();
            LastUnsuccesfulLoginDateLabel.Content += loginManagement.GetLastLogin(Storage.GetUser().Id, false) == null ? "-" : loginManagement.GetLastLogin(Storage.GetUser().Id, false).Time.ToString();

            Storage.StoredPasswordsList = PasswordsManagement.GetAllUserPasswords();

            this.listView.ItemsSource = Storage.StoredPasswordsList;

            AddPasswordButton.Click += AddPasswordButton_Click;
            EditMasterPasswordButton.Click += EditMasterPasswordButton_Click;
            LockedIPAddressesButton.Click += LockedIPAddressesButton_Click;
            SwitchModeButton.Click += SwitchModeButton_Click;
            IntroducedChanges.Click += IntroducedChanges_Click;
            ActionsHistory.Click += ActionsHistory_Click;
            LogOutButton.Click += LogOutButton_Click;
        }

        private void ActionsHistory_Click(object sender, RoutedEventArgs e)
        {
            UserAccessHistoryWindow window = new UserAccessHistoryWindow();
            window.Show();
        }

        private void IntroducedChanges_Click(object sender, RoutedEventArgs e)
        {
            IntroducedChangesWindow window = new IntroducedChangesWindow();
            window.Show();
        }

        private void SwitchModeButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchModeButton.Content = "Switch to " + Storage.applicationMode.ToString().ToLower() + " mode";
            
            Storage.applicationMode = Storage.applicationMode.Equals(Storage.Mode.Read) ? Storage.Mode.Write : Storage.Mode.Read;

            AppModeLabel.Content = "You are in " + Storage.applicationMode.ToString().ToLower() + " mode.";
        }

        private void LockedIPAddressesButton_Click(object sender, RoutedEventArgs e)
        {
            LockedIpAddressesWindow window = new LockedIpAddressesWindow();
            window.Show();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Storage.DeleteUser();
            Storage.applicationMode = Storage.Mode.Read;
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

            PasswordsManagement.SaveActionPasswordDisplayed();

            ShowPasswordWindow window = new ShowPasswordWindow(plainPassword);
            window.Show();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (Storage.applicationMode.Equals(Storage.Mode.Write))
            {
                Password password = (Password)((Button)sender).DataContext;

                if (Storage.GetUser().Id == password.IdUser)
                {
                    EditPasswordWindow window = new EditPasswordWindow(password);
                    window.Show();
                }
                else
                {
                    MessageBox.Show("You have to be an owner to edit passwords.", "Editing not allowed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
            else
            {
                MessageBox.Show("You have to switch to write mode to edit passwords.", "Read mode is active", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void SharePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            Password password = (Password)((Button)sender).DataContext;

            SharePasswordWindow window = new SharePasswordWindow(password.Id);
            window.Show();
        }

        private void DeletePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (Storage.applicationMode.Equals(Storage.Mode.Write))
            {
                Password password = (Password)((Button)sender).DataContext;

                if (Storage.GetUser().Id == password.IdUser)
                {
                    Storage.StoredPasswordsList.Remove(password);
                    PasswordsManagement.DeletePassword(password.Id);
                }
                else
                {
                    MessageBox.Show("You have to be an owner to delete passwords.", "Deleting not allowed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }               
            }
            else
            {
                MessageBox.Show("You have to switch to write mode to delete passwords.", "Read mode is active", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
