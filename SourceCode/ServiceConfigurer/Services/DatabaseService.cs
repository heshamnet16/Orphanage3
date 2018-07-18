using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.ServiceProcess;

namespace ServiceConfigurer.Services
{
    public class DatabaseService
    {
        private string _DbName;
        private string _ConnectionString;

        public delegate void PercentEventHandler(int perc);

        public event PercentEventHandler ProgressPercent;

        public DatabaseService()
        {
            _DbName = "OrphansDB";
            _ConnectionString = $"Data Source=.;Initial Catalog={_DbName};Integrated Security=True";
        }
        public async Task<bool> IsDatabaseUpdated()
        {
            if (!await IsOrphansAppLoginUserExist())
                return false;
            if (!await IsOrphansAppDBUserExist())
                return false;
            if (!await isUsersPasswordEncrypted())
                return false;
            if (!await isUserNameUniqe())
                return false;

            return true;
        }

        public async void UpdateDataBase()
        {
            if (!await IsOrphansAppLoginUserExist())
                await CreateOrphanAppLoginUser();
            if (!await IsOrphansAppDBUserExist())
                await CreateOrphanAppDbUser();
            if (!await isUsersPasswordEncrypted())
                await EncryptUsersPassword();
            if (!await isUserNameUniqe())
                await CreateUniqeConstraintOnUserName();

            await UpdateTablesColumns();
            await CleanDatabase();
        }

        public async Task<int> CheckDatabase()
        {
            int ret = 0;
            if (await IsOrphansAppLoginUserExist())
                ret = 25;
            if (await IsOrphansAppDBUserExist())
                ret += 25;
            if (await isUsersPasswordEncrypted())
                ret += 25;
            if (await isUserNameUniqe())
                ret += 25;

            return ret;
        }
        private async Task UpdateTablesColumns()
        {
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(@"ALTER TABLE Mothers 
                                                     ALTER COLUMN Dieday datetime2;", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand(@"ALTER TABLE Orphans
                                                ALTER COLUMN Weight int;", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand(@"ALTER TABLE Orphans
                                            ALTER COLUMN Tallness int;", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand(@"ALTER TABLE BondsMen
                                            ALTER COLUMN  IdentityCard_ID nvarchar(30);", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand(@"ALTER TABLE Mothers
                                            ALTER COLUMN  IdentityCard_ID nvarchar(30);", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand(@"ALTER TABLE Fathers
                                            ALTER COLUMN  IdentityCard_ID nvarchar(30);", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand(@"ALTER TABLE Orphans
                                            ALTER COLUMN  IdentityNumber nvarchar(30);", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand(@"ALTER TABLE [dbo].[Bails] DROP CONSTRAINT [Supporter_Bail]", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand(@"ALTER TABLE Bails
                                            ALTER COLUMN Supporter_ID  int NOT NULL;", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand(@"ALTER TABLE [dbo].[Bails]  WITH CHECK ADD  CONSTRAINT [Supporter_Bail] FOREIGN KEY([Supporter_ID])
                                                REFERENCES [dbo].[Supporters] ([ID])", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand(@"ALTER TABLE [dbo].[Bails] CHECK CONSTRAINT [Supporter_Bail]", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            
            sqlConnection.Close();
        }
        private async Task CleanDatabase()
        {
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($@"update [{_DbName}].[dbo].[Addresses] set Work_phone = NULL where Work_phone = '(000)0000-000' Or Work_phone = '(031)0000-000' or Work_phone = '(0000)000-000' or Work_phone='(____)___-___'", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand($@"update [{_DbName}].[dbo].[Addresses] set Fax = NULL where fax = '(000)0000-000' Or fax = '(031)0000-000' or fax = '(0000)000-000' or fax='(____)___-___'", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand($@"update [{_DbName}].[dbo].[Addresses] set Cell_Phone = NULL where Cell_Phone = '(000)0000-000' Or Cell_Phone = '(031)0000-000' or Cell_Phone = '(0000)000-000' or Cell_Phone='(____)___-___'", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand($@"update [{_DbName}].[dbo].[Addresses] set Home_Phone = NULL where Home_Phone = '(000)0000-000' Or Home_Phone = '(031)0000-000' or Home_Phone = '(0000)000-000' or Home_Phone='(____)___-___'", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand($@"update [{_DbName}].dbo.Fathers set IdentityCard_ID = NULL where IdentityCard_ID = '' or IdentityCard_ID = '0'", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand($@"update [{_DbName}].dbo.Orphans set IdentityNumber = NULL where IdentityNumber = '' or IdentityNumber = '0'", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand($@"update [{_DbName}].dbo.Mothers set IdentityCard_ID = NULL where IdentityCard_ID = '' or IdentityCard_ID = '0'", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand = new SqlCommand($@"update [{_DbName}].dbo.BondsMen set IdentityCard_ID = NULL where IdentityCard_ID = '' or IdentityCard_ID = '0'", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();

            sqlConnection.Close();
        }
        private async Task DropUniqeConstraintOnUserName()
        {
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(@"ALTER TABLE dbo.[USERS] DROP CONSTRAINT USER_NAME", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            sqlConnection.Close();
        }
        private async Task CreateUniqeConstraintOnUserName()
        {
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(@"
                                  ALTER TABLE USERS 
                                  ADD CONSTRAINT USER_NAME UNIQUE (USERNAME);", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            sqlConnection.Close();
        }
        private async Task<bool> isUserNameUniqe()
        {
            bool ret = true;
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(@"select OBJECT_ID('dbo.[USER_NAME]', 'UQ')", sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteScalarAsync();
            ret = !(sqlDataReader is null || sqlDataReader is DBNull);
            sqlConnection.Close();
            return ret;
        }
        private async Task<bool> isUsersPasswordEncrypted()
        {
            bool ret = true;
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(@"SELECT password FROM users WHERE id=1", sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteScalarAsync();
            ret = sqlDataReader.ToString().StartsWith("$OrphanHASH");
            sqlConnection.Close();
            return ret;
        }
        private async Task EncryptUsersPassword()
        {
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(@"update Users set Password='$OrphanHASH$V1$10000$Q1QQ8svGPImwbLr7nHqYEqABMUBm9DjT+94ArcYuISS5K4zuUlQ0wI2eSTNCjhBpkhfbZ2zFxMw=' where ID =1", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            sqlConnection.Close();
        }
        private async Task<bool> IsOrphansAppLoginUserExist()
        {
            bool ret = true;
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(@"SELECT loginname FROM master.dbo.syslogins WHERE NAME = 'OrphansApp'", sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteScalarAsync();
            ret = !(sqlDataReader is null || sqlDataReader is DBNull);
            sqlConnection.Close();
            return ret;
        }
        private async Task<bool> IsOrphansAppDBUserExist()
        {
            bool ret = true;
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(@"SELECT * FROM sys.database_principals WHERE name = 'OrphansApp' AND type = 'S'", sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteScalarAsync();
            ret = !(sqlDataReader is null || sqlDataReader is DBNull);
            sqlConnection.Close();
            return ret;
        }
        private async Task CreateOrphanAppDbUser()
        {
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($@"
                                   BEGIN 
                                	CREATE USER [OrphansApp] FOR LOGIN [OrphansApp] WITH DEFAULT_SCHEMA=[{_DbName}]
	                                EXEC sp_addrolemember 'db_owner', 'OrphansApp'
                                   END", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            sqlConnection.Close();
        }
        private async Task DropOrphanAppDbUser()
        {
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(@"
                                   BEGIN 
                                    DROP USER [OrphansApp]
                                   END", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            sqlConnection.Close();
        }
        private async Task CreateOrphanAppLoginUser()
        {
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(@"
                                   BEGIN 
	                                CREATE LOGIN [OrphansApp] WITH PASSWORD=N'OrphansApp3', CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
	                                ALTER LOGIN [OrphansApp] ENABLE
	                                EXEC sp_addsrvrolemember 'OrphansApp', 'dbcreator'
                                   END", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            sqlConnection.Close();
        }
        private async Task DropOrphanAppLoginUser()
        {
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(@"
                                   BEGIN 
                                    DROP LOGIN [OrphansApp]
                                   END", sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            sqlConnection.Close();
        }

        public async Task<bool> CheckDatabaseExists()
        {
            string sqlCreateDBQuery;
            bool result = false;

            try
            {
                SqlConnection tmpConn = new SqlConnection("server=(local);Trusted_Connection=yes");

                sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", _DbName);

                using (tmpConn)
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, tmpConn))
                    {
                        tmpConn.Open();

                        object resultObj = await sqlCmd.ExecuteScalarAsync();

                        int databaseID = 0;

                        if (resultObj != null)
                        {
                            int.TryParse(resultObj.ToString(), out databaseID);
                        }

                        tmpConn.Close();

                        result = (databaseID > 0);
                    }
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public async Task<bool> CheckSQLServerExists()
        {
            string sqlCreateDBQuery;
            bool result = false;

            try
            {
                SqlConnection tmpConn = new SqlConnection("server=(local);Trusted_Connection=yes");

                sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases");

                using (tmpConn)
                {
                    await tmpConn.OpenAsync();
                    return true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public void Backup(string _BackupFileName)
        {
            Backup bkpDBFull = new Backup();

            bkpDBFull.Action = BackupActionType.Database;

            bkpDBFull.Database = _DbName;

            bkpDBFull.Devices.AddDevice(_BackupFileName, DeviceType.File);
            bkpDBFull.BackupSetName = "Database backup";
            bkpDBFull.BackupSetDescription = string.Format("Database: {0}. Date: {1}.",
                    _DbName, DateTime.Now.ToString("dd.MM.yyyy hh:m")); ;
            bkpDBFull.Initialize = false;
            bkpDBFull.MediaDescription = "Disk";
            bkpDBFull.PercentComplete += CompletionStatusInPercent;

            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            sqlConnection.Open();

            ServerConnection sc = new ServerConnection(sqlConnection);
            Server MyServer = new Server(sc);
            bkpDBFull.SqlBackup(MyServer);

            sqlConnection.Close();
        }
        public void Restore(string _BackupFileName)
        {
            RestartTheService();
            Restore dbRestore = new Restore();
            ServerConnection sc = new ServerConnection();
            dbRestore.Database = _DbName;
            dbRestore.Action = RestoreActionType.Database;
            dbRestore.ReplaceDatabase = true;
            BackupDeviceItem device = new BackupDeviceItem
                (_BackupFileName, DeviceType.File);
            dbRestore.PercentComplete +=
            new PercentCompleteEventHandler(CompletionStatusInPercent);
            sc.ServerInstance = ".";
            Server MyServer = new Server(sc);
            try
            {
                dbRestore.Devices.Add(device);

                RelocateFile DataFile = new RelocateFile();
                //string MDF = dbRestore.ReadFileList(MyServer).Rows[0][1].ToString();
                DataFile.LogicalFileName = dbRestore.ReadFileList(MyServer).Rows[0][0].ToString();
                DataFile.PhysicalFileName = MyServer.Databases[_DbName].FileGroups[0].Files[0].FileName;

                RelocateFile LogFile = new RelocateFile();
                //string LDF = dbRestore.ReadFileList(MyServer).Rows[1][1].ToString();
                LogFile.LogicalFileName = dbRestore.ReadFileList(MyServer).Rows[1][0].ToString();
                LogFile.PhysicalFileName = MyServer.Databases[_DbName].LogFiles[0].FileName;

                dbRestore.RelocateFiles.Add(DataFile);
                dbRestore.RelocateFiles.Add(LogFile);

                dbRestore.SqlRestore(MyServer);
                sc.Disconnect();
            }
            catch (Exception exc)
            {
                dbRestore.Abort();
                throw exc;
            }
        }

        private void CompletionStatusInPercent(object sender, PercentCompleteEventArgs args)
        {
            ProgressPercent?.Invoke(args.Percent);
        }

        private void RestartTheService()
        {
            ServiceController serviceController = new ServiceController("MSSQLSERVER");
            serviceController.Stop();
            serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
            serviceController.Start();
            serviceController.WaitForStatus(ServiceControllerStatus.Running);
        }
    }
}
