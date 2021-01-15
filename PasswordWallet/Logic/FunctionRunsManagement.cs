using PasswordWallet.Controllers;
using PasswordWallet.Database.DbModels;
using PasswordWallet.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Logic
{
    public class FunctionRunsManagement
    {
        private FunctionRunsController _functionRunsController = new FunctionRunsController();

        public ObservableCollection<FunctionToDisplay> GetFunctionRunsForUser(int id, string regex)
        {
            return _functionRunsController.GetAllFunctionRunsForUser(id, regex);
        }
    }
}
