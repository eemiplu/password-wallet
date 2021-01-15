using PasswordWallet.Controllers;
using PasswordWallet.Cryptography;
using PasswordWallet.Database.DbModels;
using PasswordWallet.DbModels;
using PasswordWallet.Logic.HelperClasses;
using System;
using System.Collections.ObjectModel;

namespace PasswordWallet.Logic
{
    public class PasswordsManagement
    {
        private static PasswordsController _passwordsController = new PasswordsController();

        private static SharedPasswordsController _sharedPasswordsController = new SharedPasswordsController();

        private static UsersController _usersController = new UsersController();

        private static DataChangesController _dataChangesController = new DataChangesController();

        private static FunctionRunsController _functionRunsController = new FunctionRunsController();

        public static Password AddNewPassword(Password password)
        {
            RecordSerializer serializer = new RecordSerializer();

            password.IdUser = Storage.GetUser().Id;

            EncryptionManager encryptionManager = new EncryptionManager();
            password.PasswordHash = encryptionManager.AESEncrypt(Storage.GetUser().Salt, password.PasswordHash);

            password = _passwordsController.AddPassword(password);

            _functionRunsController.Add(new FunctionRun() { IdUser = Storage.GetUser().Id, Time = DateTime.Now, IdFunction = (int)Enums.Function.AddPassword });
            _dataChangesController.Add(new DataChange() { IdUser = Storage.GetUser().Id, IdModifiedRecord = password.Id, PresentValueOfRecord = serializer.PasswordObjectToString(password), Time = DateTime.Now, IdActionType = (int)Enums.ActionType.create, IdTableName = (int)Enums.TableName.Passwords });

            return password;
        }

        public static void DeletePassword(int id)
        {
            RecordSerializer serializer = new RecordSerializer();

            Password password = _passwordsController.GetPassword(id);

            _passwordsController.DeletePassword(id);

            _functionRunsController.Add(new FunctionRun() { IdUser = Storage.GetUser().Id, Time = DateTime.Now, IdFunction = (int)Enums.Function.DeletePassword });
            _dataChangesController.Add(new DataChange() { IdUser = Storage.GetUser().Id, IdModifiedRecord = id, PreviousValueOfRecord = serializer.PasswordObjectToString(password), Time = DateTime.Now, IdActionType = (int)Enums.ActionType.delete, IdTableName = (int)Enums.TableName.Passwords });
        }

        public static Password ChangePassword(Password previous, Password password)
        {
            RecordSerializer serializer = new RecordSerializer();

            EncryptionManager encryptionManager = new EncryptionManager();
            password.PasswordHash = encryptionManager.AESEncrypt(Storage.GetUser().Salt, password.PasswordHash);

            password = _passwordsController.UpdatePassword(password);

            _functionRunsController.Add(new FunctionRun() { IdUser = Storage.GetUser().Id, Time = DateTime.Now, IdFunction = (int)Enums.Function.UpdatePassword });
            _dataChangesController.Add(new DataChange() { IdUser = Storage.GetUser().Id, IdModifiedRecord = password.Id, PreviousValueOfRecord = serializer.PasswordObjectToString(previous), PresentValueOfRecord = serializer.PasswordObjectToString(password), Time = DateTime.Now, IdActionType = (int)Enums.ActionType.update, IdTableName = (int)Enums.TableName.Passwords });

            return password;
        }

        public static ObservableCollection<Password> GetAllUserPasswords()
        {
            return _passwordsController.GetAllPasswordsForUser(Storage.GetUser().Id);
        }

        public static void UpdateAllUserPasswords(String oldSalt, String newSalt)
        {
            foreach (Password pass in Storage.StoredPasswordsList)
            {
                EncryptionManager encryptionManager = new EncryptionManager();
                string plainPassword = encryptionManager.AESDecrypt(oldSalt, pass.PasswordHash);

                pass.PasswordHash = encryptionManager.AESEncrypt(newSalt, plainPassword);

                _passwordsController.UpdatePassword(pass);
            }
        }

        public SharedPassword SharePassword(string login, int idPasword)
        {
            SharedPassword sharedPassword = null;

            if (_passwordsController.GetPassword(idPasword).IdUser.Equals(Storage.GetUser().Id))
            {
                User user = _usersController.GetUser(login);

                if (user != null)
                {
                    sharedPassword = _sharedPasswordsController.AddSharedPassword(new SharedPassword(user.Id, idPasword));

                    _functionRunsController.Add(new FunctionRun() { IdUser = Storage.GetUser().Id, Time = DateTime.Now, IdFunction = (int)Enums.Function.SharePassword });
                }
                else
                {
                    sharedPassword = null;
                }
            }

            return sharedPassword;
        }

        public static void SaveActionPasswordDisplayed()
        {
            _functionRunsController.Add(new FunctionRun() { IdUser = Storage.GetUser().Id, Time = DateTime.Now, IdFunction = (int)Enums.Function.ShowPassword });
        }
    }
}
