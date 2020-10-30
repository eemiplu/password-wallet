using System;
using System.Windows;

namespace PasswordWallet.Views
{
    public partial class ShowPasswordWindow : Window
    {
        public ShowPasswordWindow(String password)
        {
            InitializeComponent();

            PasswordTextBox.Text = password;

            OKButton.Click += OKButton_Click;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
