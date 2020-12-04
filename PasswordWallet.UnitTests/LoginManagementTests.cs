using NUnit.Framework;
using PasswordWallet.Database.DbModels;
using PasswordWallet.Logic;
using PasswordWallet.UnitTests.Adapters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Tests
{
    public class LoginManagementTests
    {
        [Test]
        [TestCase(2, 5)]
        [TestCase(3, 10)]
        [TestCase(1, 0)]
        public void getVerificationTime_Int_IncorrectLoginTrialsInRange(int incorrectLoginTrials, int expResult)
        {
            LoginManagement instance = new LoginManagement();

            int result = instance.getVerificationTime(incorrectLoginTrials);

            Assert.AreEqual(expResult, result);
        }

        private static IEnumerable<TestCaseData> findLastUserLogin_LastCorrectLoginDate
        {
            get
            {
                var c = new List<Login>();
                c.Add(new Login(1, new DateTime(2020, 04, 12, 12, 34, 30), 1, true));
                c.Add(new Login(1, new DateTime(2020, 04, 11, 12, 37, 30), 1, false));
                c.Add(new Login(1, new DateTime(2020, 04, 12, 15, 37, 30), 1, true));
                yield return new TestCaseData(new ObservableCollection<Login>(c), true);
            }
        }

        [Test]
        [TestCaseSource("findLastUserLogin_LastCorrectLoginDate")]
        public void findLastUserLogin_LastCorrectLoginDate_LastCorrectLoginDateExists(ObservableCollection<Login> userLogins, bool succesful)
        {
            LoginManagement instance = new LoginManagement();

            Login result = instance.findLastUserLogin(userLogins, succesful);

            Assert.AreEqual(new DateTime(2020, 04, 12, 15, 37, 30), result.Time);
        }

        private static IEnumerable<TestCaseData> findLastUserLogin_LastIncorrectLoginDate
        {
            get
            {
                var c = new List<Login>();
                c.Add(new Login(1, new DateTime(2020, 04, 12, 12, 34, 30), 1, true));
                c.Add(new Login(1, new DateTime(2020, 04, 17, 15, 12, 00), 1, false));
                c.Add(new Login(1, new DateTime(2020, 04, 11, 09, 10, 00), 1, false));
                yield return new TestCaseData(new ObservableCollection<Login>(c), false);
            }
        }

        [Test]
        [TestCaseSource("findLastUserLogin_LastIncorrectLoginDate")]
        public void findLastUserLogin_LastIncorrectLoginDate_LastIncorrectLoginDateExists(ObservableCollection<Login> userLogins, bool succesful)
        {
            LoginManagement instance = new LoginManagement();

            Login result = instance.findLastUserLogin(userLogins, succesful);

            Assert.AreEqual(new DateTime(2020, 04, 17, 15, 12, 00), result.Time);
        }

        private static IEnumerable<TestCaseData> CheckIfIPAddressIsBlocked_True
        {
            get
            {
                yield return new TestCaseData(new IPAddress() { Id = 5, IpAddress = "192.168.2.3", IncorrectLoginTrials = 4 });
                yield return new TestCaseData(new IPAddress() { Id = 2, IpAddress = "192.168.0.13", IncorrectLoginTrials = 6 });
            }
        }

        [Test]
        [TestCaseSource("CheckIfIPAddressIsBlocked_True")]
        public void CheckIfIPAddressIsBlocked_True_AddressIsBlocked(IPAddress ipAddress)
        {
            LoginManagement instance = new LoginManagement();

            bool result = instance.CheckIfIPAddressIsBlocked(ipAddress);

            Assert.AreEqual(true, result);
        }

        private static IEnumerable<TestCaseData> CheckIfIPAddressIsBlocked_False
        {
            get
            {
                yield return new TestCaseData(new IPAddress("192.168.1.2"));
                yield return new TestCaseData(new IPAddress() { Id = 5, IpAddress = "192.168.2.3", IncorrectLoginTrials = 1 });
                yield return new TestCaseData(new IPAddress() { Id = 5, IpAddress = "192.168.2.3", IncorrectLoginTrials = 2 });
                yield return new TestCaseData(new IPAddress() { Id = 5, IpAddress = "192.168.2.3", IncorrectLoginTrials = 3 });
            }
        }

        [Test]
        [TestCaseSource("CheckIfIPAddressIsBlocked_False")]
        public void CheckIfIPAddressIsBlocked_False_AddressIsBlocked(IPAddress ipAddress)
        {
            LoginManagement instance = new LoginManagement();

            bool result = instance.CheckIfIPAddressIsBlocked(ipAddress);

            Assert.AreEqual(false, result);
        }
    }
}