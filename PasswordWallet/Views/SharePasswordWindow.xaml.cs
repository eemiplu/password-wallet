using PasswordWallet.Logic;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for SharePasswordWindow.xaml
    /// </summary>
    public partial class SharePasswordWindow : Window
    {
        PasswordsManagement passwordsManagement = new PasswordsManagement();
        int passwordId;

        public SharePasswordWindow(int passwordId)
        {
            InitializeComponent();

            this.passwordId = passwordId;

            ShareButton.Click += ShareButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShareButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text != "" && UsernameTextBox.Text != Storage.GetUser().Login)
            {
                var result = passwordsManagement.SharePassword(UsernameTextBox.Text, passwordId); //spr czy jest taki user w funkcji

                if (result == null)
                {
                    MessageBox.Show("User with given login doesn't exists.", "Incorrect Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid login.", "Incorrect Login", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
