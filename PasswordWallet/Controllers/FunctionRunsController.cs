using PasswordWallet.Database.DbModels;
using PasswordWallet.DataModels;
using PasswordWallet.DbModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordWallet.Controllers
{
    public class FunctionRunsController
    {
        private DBContext db = new DBContext();

        public FunctionRun Add(FunctionRun functionRun)
        {
            try
            {
                db.FunctionRuns.Add(functionRun);
                db.SaveChanges();

                return functionRun;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Adding dataChange to database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return null;
        }

        public ObservableCollection<FunctionToDisplay> GetAllFunctionRunsForUser(int id, string regex)
        {
            var query = (from fr in db.FunctionRuns
                     join f in db.Functions on fr.IdFunction equals f.Id
                     where fr.IdUser == id && f.FunctionName.Contains(regex)
                     select new FunctionToDisplay { 
                        Id = fr.Id,
                        Time = fr.Time,
                        FunctionName = f.FunctionName,
                        Description = f.Description
                     });

            return new ObservableCollection<FunctionToDisplay>(query);
        }
    }
}
