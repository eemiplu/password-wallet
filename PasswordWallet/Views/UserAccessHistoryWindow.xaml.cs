using PasswordWallet.Database.DbModels;
using PasswordWallet.DataModels;
using PasswordWallet.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UserAccessHistory.xaml
    /// </summary>
    public partial class UserAccessHistoryWindow : Window
    {
        ObservableCollection<FunctionToDisplay> functionList;
        FunctionRunsManagement functionRunsManagement = new FunctionRunsManagement();

        public UserAccessHistoryWindow()
        {
            InitializeComponent();

            functionList = functionRunsManagement.GetFunctionRunsForUser(Storage.GetUser().Id, "");
            this.listView.ItemsSource = functionList;

            SearchButton.Click += SearchButton_Click;
            CloseButton.Click += CloseButton_Click;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            functionList = functionRunsManagement.GetFunctionRunsForUser(Storage.GetUser().Id, SearchTextBox.Text);
            this.listView.ItemsSource = functionList;

            SearchTextBox.Text = "";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
