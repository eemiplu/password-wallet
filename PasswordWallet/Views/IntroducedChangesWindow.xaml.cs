using PasswordWallet.DataModels;
using PasswordWallet.DbModels;
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
    /// Interaction logic for IntroducedChanges.xaml
    /// </summary>
    public partial class IntroducedChangesWindow : Window
    {
        ObservableCollection<DataChangeDisplay> functionList;
        DataChangesManagement dataChangesManagement = new DataChangesManagement();
        PasswordsManagement passwordsManagement = new PasswordsManagement();

        public IntroducedChangesWindow()
        {
            InitializeComponent();

            functionList = dataChangesManagement.GetDataChangesForUser(Storage.GetUser().Id, "");
            this.listView.ItemsSource = functionList;

            SearchButton.Click += SearchButton_Click;
            CloseButton.Click += CloseButton_Click;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            functionList = dataChangesManagement.GetDataChangesForUser(Storage.GetUser().Id, SearchTextBox.Text);
            this.listView.ItemsSource = functionList;

            SearchTextBox.Text = "";
        }

        private void RecoverRecordButton_Click(object sender, RoutedEventArgs e)
        {
            DataChangeDisplay dataChange = (DataChangeDisplay)((Button)sender).DataContext;

            Password password = dataChangesManagement.PasswordRecordRecovery(dataChange.Id);

            functionList = dataChangesManagement.GetDataChangesForUser(Storage.GetUser().Id, SearchTextBox.Text);
            this.listView.ItemsSource = functionList;

            Storage.StoredPasswordsList.Remove(Storage.StoredPasswordsList.First(p => p.Id == password.Id));
            Storage.StoredPasswordsList.Add(password);
        }
    }
}
