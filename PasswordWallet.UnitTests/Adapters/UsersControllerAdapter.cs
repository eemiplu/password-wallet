using PasswordWallet.Controllers.Interfaces;
using PasswordWallet.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordWallet.UnitTests.Adapters
{
    public class UsersControllerAdapter : IUsersController
    {
        public User GetUser(string login)
        {
            return new User("test", "7C2855530B5522DF146BB0DC81A367BD12B057F989CD2E51FF3BCC2145A90985F7C1D1626F9163AD3CC2FBAA78CAC5B3EFDEC10C5E27F12953FBCFA9D8350B99", "lM2sgUHBDqkKD0yOPSkyCA==", true);
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
