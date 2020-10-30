using PasswordWallet.Cryptography;
using PasswordWallet.DbModels;
using PasswordWallet.Logic;
using System.Linq;
using System.Windows;

namespace PasswordWallet.Views
{
    public partial class EditPasswordWindow : Window
    {
        Password _password = new Password();

        public EditPasswordWindow(Password password = null)
        {
            InitializeComponent();

            if (password != null)
            {
                EncryptionManager encryptionManager = new EncryptionManager();

                PasswordTextBox.Password = encryptionManager.AESDecrypt(Storage.GetUser().Salt, password.PasswordHash);
                LoginTextBox.Text = password.Login;
                WebAddressTextBox.Text = password.WebAddress;
                DescriptionTextBox.Text = password.Description;

                _password = password;
            }

            SaveButton.Click += SaveButton_Click;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Password == "") return;

            _password.PasswordHash = PasswordTextBox.Password;
            _password.Login = LoginTextBox.Text;
            _password.WebAddress = WebAddressTextBox.Text;
            _password.Description = DescriptionTextBox.Text;

            if (_password.Id > 0 )
            {
                if (PasswordsManagement.ChangePassword(_password) != null)
                {
                    Storage.StoredPasswordsList.Remove(Storage.StoredPasswordsList.First(p => p.Id == _password.Id));
                    Storage.StoredPasswordsList.Add(_password);
                    this.Close();
                }
            }
            else
            {
                Password pass = PasswordsManagement.AddNewPassword(_password);
                if (pass != null)
                {
                    Storage.StoredPasswordsList.Add(pass);
                    this.Close();
                }
            }
        }
    }
}
