using PasswordWallet.Controllers;
using PasswordWallet.Database.DbModels;
using PasswordWallet.DataModels;
using PasswordWallet.DbModels;
using PasswordWallet.Logic.HelperClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Logic
{
    public class DataChangesManagement
    {
        private static PasswordsController _passwordsController = new PasswordsController();

        private static DataChangesController _dataChangesController = new DataChangesController();

        public Password PasswordRecordRecovery(int dataChangeId)
        {
            DataChange dataChange = _dataChangesController.GetDataChange(dataChangeId);

            RecordSerializer serializer = new RecordSerializer();
            Password password = serializer.StringToPasswordObject(dataChange.PreviousValueOfRecord);

            Password previous = _passwordsController.GetPassword(password.Id);

            Password updatedPass = _passwordsController.UpdatePassword(password);

            _dataChangesController.Add(new DataChange() { IdUser = Storage.GetUser().Id, IdModifiedRecord = password.Id, PreviousValueOfRecord = serializer.PasswordObjectToString(previous), 
                PresentValueOfRecord = serializer.PasswordObjectToString(password), Time = DateTime.Now, IdActionType = (int)Enums.ActionType.recovery, 
                IdTableName = (int)Enums.TableName.Passwords });

            return updatedPass;
        }

        public ObservableCollection<DataChangeDisplay> GetDataChangesForUser(int id, string regex)
        {
            return _dataChangesController.GetAllDataChangesForUser(id, regex);
        }
    }
}
