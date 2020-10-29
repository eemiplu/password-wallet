﻿using System;
using System.Windows;
using PasswordWallet.DbModels;
using PasswordWallet.Views;
using PasswordWallet.Logic;

namespace PasswordWallet
{
    public partial class MainWindow : Window
    {
        String _login;
        String _password;

        public MainWindow()
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

            if (UserManagement.CheckLoginData(_login.Trim(), _password) != null)
            {
                //store user
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
