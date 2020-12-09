using System;
using System.Windows;
using PasswordWallet.DbModels;
using PasswordWallet.Views;
using PasswordWallet.Logic;
using PasswordWallet.Logic.HelperClasses;
using PasswordWallet.Database.DbModels;
using System.Windows.Threading;

namespace PasswordWallet
{
    public partial class LoginWindow : Window
    {
        String _login;
        String _password;
        DateTime lastLoginTrialTime = DateTime.MinValue;
        int verificationTime;
        DispatcherTimer dispatcherTimer;

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
            LoginManagement loginManagement = new LoginManagement();
            string ip = IpManager.GetLocalIP();
            IpAddressManagement ipManagement = new IpAddressManagement();
            IPAddress ipAddress = ipManagement.AddNewIp(ip);
            Storage.IpAddress = ipAddress;

            _login = LoginTextBox.Text;
            _password = PasswordTextBox.Password;

            if (loginManagement.CheckIfIPAddressIsBlocked(Storage.IpAddress))
            {
                MessageTextBlock.Text = "This IP address is permanently blocked \nbecause of 4 failed login trials.";
                MessageTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                int verTime = loginManagement.UserVerificationTimeInSeconds(Storage.IpAddress.IpAddress, _login);

                if ((verTime > 0 && lastLoginTrialTime.AddSeconds(verTime) < DateTime.Now) || verTime == 0)
                {
                    MessageTextBlock.Visibility = Visibility.Hidden;
                    lastLoginTrialTime = DateTime.MinValue;

                    if (VerifyUser())
                    {
                        PasswordList window = new PasswordList();
                        window.Show();
                        this.Close();
                    }
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e, int incorrectTrials)
        {
            verificationTime--;

            MessageTextBlock.Text = "You provided incorrect login data " + incorrectTrials
                        + " times. \nTry again in " + verificationTime + "s.";

            if (verificationTime == 0)
            {
                dispatcherTimer.Stop();
                MessageTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private bool validateForm()
        {
            if (!isPasswordValid(_password) || String.IsNullOrEmpty(_login))
            {
                return false;
            }

            return true;
        }

        private bool VerifyUser()
        {
            LoginManagement loginManagement = new LoginManagement();
            UserManagement userManagement = new UserManagement();

            if (!validateForm())
            {
                MessageBox.Show("Fill in the fields correctly. Password must be at least 8 characters long.", "Incorrect data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            User loggedUser = loginManagement.Login(_login.Trim(), _password);

            if (loggedUser != null)
            {
                Storage.applicationMode = Storage.Mode.Read;
                Storage.CreateUser(loggedUser);
                return true;
            }
            else
            {
                MessageBox.Show("Invalid login or password.", "User not found", MessageBoxButton.OK, MessageBoxImage.Warning);

                string ip = IpManager.GetLocalIP();
                IpAddressManagement ipManagement = new IpAddressManagement();
                IPAddress ipAddress = ipManagement.AddNewIp(ip);
                Storage.IpAddress = ipAddress;

                if (!loginManagement.CheckIfIPAddressIsBlocked(Storage.IpAddress))
                {
                    int verTime = loginManagement.UserVerificationTimeInSeconds(Storage.IpAddress.IpAddress, _login);

                    if (verTime > 0 && lastLoginTrialTime == DateTime.MinValue)
                    {
                        lastLoginTrialTime = DateTime.Now;

                        verificationTime = verTime;

                        int incorrectTrials = Math.Max(Storage.IpAddress.IncorrectLoginTrials, userManagement.getUser(_login).IncorrectLogins);

                        MessageTextBlock.Text = "You provided incorrect login data " + incorrectTrials
                            + " times. \nTry again in " + verificationTime + "s.";
                        MessageTextBlock.Visibility = Visibility.Visible;

                        dispatcherTimer = new DispatcherTimer();
                        dispatcherTimer.Tick += (sender, e) => dispatcherTimer_Tick(sender, e, incorrectTrials); //new EventHandler(dispatcherTimer_Tick);
                        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                        dispatcherTimer.Start();
                    }
                }
                else
                {
                    MessageTextBlock.Text = "This IP address is permanently blocked \nbecause of 4 failed login trials.";
                    MessageTextBlock.Visibility = Visibility.Visible;
                }
            }

            return false;
        }

        private delegate void NoArgDelegate();

        public static void Refresh(DependencyObject obj)
        {
            obj.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, (NoArgDelegate)delegate { });
        }

        private bool isPasswordValid(String password)
        {
            return password.Length >= 8;
        }
    }
}
