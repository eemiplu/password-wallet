using PasswordWallet.Logic;
using System.Linq;
using System.Windows;

namespace PasswordWallet.Views
{
    public partial class ChangeUserPasswordWindow : Window
    {
        string _oldPass;
        string _newPass;
        string _repeatedPass;
        bool _passwordShouldBeStoredAsHash;

        public ChangeUserPasswordWindow()
        {
            InitializeComponent();

            SaveButton.Click += SaveButton_Click;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _oldPass = OldPasswordTextBox.Password;
            _newPass = NewPasswordTextBox.Password;
            _repeatedPass = RepeatedPasswordTextBox.Password;
            _passwordShouldBeStoredAsHash = Sha512Radio.IsChecked == true ? true : false;

            if (UserManagement.CheckLoginData(Storage.GetUser().Login, _oldPass) == null)
            {
                MessageBox.Show("Password is incorrect.", "Wrong password", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (_oldPass.Length < 8 || _newPass.Length < 8 || _repeatedPass.Length < 8)
            {
                MessageBox.Show("Fill in the fields correctly. Password must be at least 8 characters long.", "Incorrect data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!_newPass.Equals(_repeatedPass))
            {
                MessageBox.Show("Given passwords are different.", "Different passwords", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (UserManagement.ChangePassword(_newPass, _passwordShouldBeStoredAsHash) != null)
            {
                var window = Application.Current.Windows.OfType<PasswordList>().SingleOrDefault(w => w.IsLoaded);

                window.listView.ItemsSource = null;
                window.listView.ItemsSource = Storage.StoredPasswordsList;

                this.Close();
            }
        }
    }
}
