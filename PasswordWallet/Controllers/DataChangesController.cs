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
    public class DataChangesController
    {
        private DBContext db = new DBContext();

        public DataChange Add(DataChange dataChange)
        {
            try
            {
                db.DataChanges.Add(dataChange);
                db.SaveChanges();

                return dataChange;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured: " + ex.Message, "Adding dataChange to database failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return null;
        }

        public DataChange GetDataChange(int id)
        {
            return db.DataChanges.FirstOrDefault(u => u.Id == id);
        }

        public ObservableCollection<DataChangeDisplay> GetAllDataChangesForUser(int id, string regex)
        {
            var query = (from dc in db.DataChanges
                         join tn in db.TableNames on dc.IdTableName equals tn.Id
                         join a in db.ActionTypes on dc.IdActionType equals a.Id
                         where dc.IdUser == id && a.Title.Contains(regex)
                         select new DataChangeDisplay
                         {
                             Id = dc.Id,
                             IdModifiedRecord = dc.IdModifiedRecord,
                             Time = dc.Time,
                             PreviousValueOfRecord = dc.PreviousValueOfRecord,
                             PresentValueOfRecord = dc.PresentValueOfRecord,
                             ActionType = a.Title,
                             TableName = tn.Table_Name
                         });

            return new ObservableCollection<DataChangeDisplay>(query);
        }
    }
}
