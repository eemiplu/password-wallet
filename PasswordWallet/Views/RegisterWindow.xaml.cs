using PasswordWallet.Logic;
using System;
using System.Windows;

namespace PasswordWallet.Views
{
    public partial class RegisterWindow : Window
    {
        String _login;
        String _password;
        bool _passwordShouldBeStoredAsHash;

        public RegisterWindow()
        {
            InitializeComponent();

            RegisterButton.Click += RegisterButton_Click;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            UserManagement userManagement = new UserManagement();

            _login = LoginTextBox.Text;
            _password = PasswordTextBox.Password;
            _passwordShouldBeStoredAsHash = Sha512Radio.IsChecked == true ? true : false;

            if (!VerifyData())
            {
                return;
            }

            if (userManagement.RegisterNewUser(_login.Trim(), _password, _passwordShouldBeStoredAsHash) != null)
            {
                LoginWindow window = new LoginWindow();
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("User with given login already exists. Please choose another login.", "Login is not available", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool VerifyData()
        {
            if (!isPasswordValid(_password) || String.IsNullOrEmpty(_login))
            {
                MessageBox.Show("Fill in the fields correctly. Password must be at least 8 characters long.", "Incorrect data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private bool isPasswordValid(String password)
        {
            return password.Length >= 8;
        }
    }
}
