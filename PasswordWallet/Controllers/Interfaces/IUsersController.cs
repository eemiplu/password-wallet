using PasswordWallet.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Controllers.Interfaces
{
    public interface IUsersController
    {
        User GetUser(String login);
        User UpdateUser(User user);
    }
}
