using System;
using System.Windows;
using PasswordWallet.DbModels;
using PasswordWallet.Views;
using PasswordWallet.Logic;
using PasswordWallet.Cryptography;
using System.Net.Sockets;
using System.Net;

namespace PasswordWallet
{
    public partial class LoginWindow : Window
    {
        String _login;
        String _password;

        public LoginWindow()
        {
            InitializeComponent();

            LogInButton.Click += LogInButton_Click;
            RegisterButton.Click += RegisterButton_Click;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow();
            window.Show();
            this.Close();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            Storage.GetLocalIP();

            _login = LoginTextBox.Text;
            _password = PasswordTextBox.Password;

            if (VerifyUser())
            {
                PasswordList window = new PasswordList();
                window.Show();
                this.Close();
            }
        }

        private bool VerifyUser()
        {
            if (!isPasswordValid(_password) || String.IsNullOrEmpty(_login))
            {
                MessageBox.Show("Fill in the fields correctly. Password must be at least 8 characters long.", "Incorrect data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            User user = UserManagement.CheckLoginData(_login.Trim(), _password);

            if (user != null)
            {
                Storage.CreateUser(user);
                return true;
            }

            MessageBox.Show("Invalid login or password.", "User not found", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        private bool isPasswordValid(String password)
        {
            return password.Length >= 8;
        }
    }
}
