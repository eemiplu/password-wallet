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
        Password _previous = new Password();

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
                _previous = new Password() { Id = password.Id, PasswordHash = password.PasswordHash, IdUser = password.IdUser, WebAddress = password.WebAddress, Description = password.Description, Login = password.Login, Deleted = password.Deleted };
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
                if (PasswordsManagement.ChangePassword(_previous, _password) != null)
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
