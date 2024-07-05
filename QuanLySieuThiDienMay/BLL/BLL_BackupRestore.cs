using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class BLL_BackupRestore
    {
        QLSieuThiDataContext qlst = new QLSieuThiDataContext();

        //public string BackupDatabase(string backupDirectory)
        //{
        //    if (!Directory.Exists(backupDirectory))
        //    {
        //        Directory.CreateDirectory(backupDirectory);
        //    }

        //    string backupFileName = Path.Combine(backupDirectory, $"SieuThi9_{DateTime.Now:yyyyMMdd_HHmmss}.bak");
        //    string backupQuery = $"BACKUP DATABASE [SieuThi9] TO DISK = '{backupFileName}' WITH NOFORMAT, INIT, NAME = 'SieuThi9-Full Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";

        //    using (SqlConnection connection = new SqlConnection(qlst.Connection.ConnectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(backupQuery, connection))
        //        {
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }

        //    return backupFileName;
        //}

        //public void RestoreDatabase(string backupFileName)
        //{
        //    string restoreQuery = $"USE master; ALTER DATABASE [SieuThi9] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE [SieuThi9] FROM DISK = '{backupFileName}' WITH REPLACE; ALTER DATABASE [SieuThi9] SET MULTI_USER;";

        //    using (SqlConnection connection = new SqlConnection(qlst.Connection.ConnectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(restoreQuery, connection))
        //        {
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        public async Task<DataTable> GetSqlServersAsync()
        {
            return await Task.Run(() =>
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ServerName");
                dataTable.Columns.Add("InstanceName");
                string[] possibleServerNames = { "localhost", ".", "(local)", Environment.MachineName };

                foreach (string serverName in possibleServerNames)
                {
                    try
                    {
                        string connectionString = $"Server={serverName};Integrated Security=True;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            dataTable.Rows.Add(serverName);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Kết nối thất bại tới: {serverName}: {ex.Message}");
                    }
                }

                return dataTable;
            });
        }

        public async Task<DataTable> GetDatabasesAsync(string serverName, string userId, string password)
        {
            return await Task.Run(() =>
            {
                DataTable databasesTable = new DataTable();
                databasesTable.Columns.Add("database_name");

                string connectionString;

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
                {
                    connectionString = $"Server={serverName};Database=master;Integrated Security=True;";
                }
                else
                {
                    connectionString = $"Server={serverName};Database=master;User Id={userId};Password={password};";
                }

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT name FROM sys.databases WHERE state_desc = 'ONLINE'";
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string databaseName = reader["name"].ToString();
                            databasesTable.Rows.Add(databaseName);
                        }

                        reader.Close();
                        return databasesTable;
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Đã xảy ra lỗi khi truy xuất cơ sở dữ liệu.", ex);
                }
            });
        }

        //public async Task RestoreDatabaseAsync(string backupFileName, string serverName, string databaseName, string userId, string password)
        //{
        //    await Task.Run(() =>
        //    {
        //        string connectionString;

        //        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
        //        {
        //            connectionString = $"Server={serverName};Database=master;Integrated Security=True;";
        //        }
        //        else
        //        {
        //            connectionString = $"Server={serverName};Database=master;User Id={userId};Password={password};";
        //        }

        //        string restoreQuery = $@"
        //    ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
        //    RESTORE DATABASE [{databaseName}] FROM DISK = @backupFileName WITH REPLACE;
        //    ALTER DATABASE [{databaseName}] SET MULTI_USER;";

        //        try
        //        {
        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                using (SqlCommand command = new SqlCommand(restoreQuery, connection))
        //                {
        //                    command.Parameters.AddWithValue("@backupFileName", backupFileName);
        //                    connection.Open();
        //                    command.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log or handle the exception as needed
        //            throw new ApplicationException("An error occurred while restoring the database.", ex);
        //        }
        //    });
        //}

        public async Task RestoreDatabaseAsync(string backupFileName, string serverName, string databaseName, string userId, string password)
        {
            await Task.Run(() =>
            {
                string connectionString;

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
                {
                    connectionString = $"Server={serverName};Database=master;Integrated Security=True;";
                }
                else
                {
                    connectionString = $"Server={serverName};Database=master;User Id={userId};Password={password};";
                }

                string restoreQuery = $@"
                ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                RESTORE DATABASE [{databaseName}] FROM DISK = @backupFileName WITH REPLACE;
                ALTER DATABASE [{databaseName}] SET MULTI_USER;";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Kill all connections to the database
                        SqlCommand killConnectionsCommand = new SqlCommand($@"
                        DECLARE @kill varchar(8000) = '';
                        SELECT @kill = @kill + 'KILL ' + CONVERT(varchar(5), session_id) + ';'
                        FROM sys.dm_exec_sessions
                        WHERE database_id  = DB_ID('{databaseName}')

                        EXEC(@kill);", connection);

                        killConnectionsCommand.ExecuteNonQuery();

                        using (SqlCommand command = new SqlCommand(restoreQuery, connection))
                        {
                            command.Parameters.AddWithValue("@backupFileName", backupFileName);
                            command.CommandTimeout = 60 * 5; // 5 minutes timeout for the command
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (SqlException ex)
                {

					MessageBox.Show($"Đã xảy ra lỗi : {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
            });
        }

        public string BackupDatabase(string backupDirectory)
        {
            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            }

            string backupFileName = Path.Combine(backupDirectory, $"SieuThi_{DateTime.Now:yyyyMMdd_HHmmss}.bak");
            string backupQuery = $"BACKUP DATABASE [SieuThi] TO DISK = '{backupFileName}' WITH NOFORMAT, INIT, NAME = 'SieuThi-Full Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";

            using (SqlConnection connection = new SqlConnection(qlst.Connection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(backupQuery, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return backupFileName;
        }

    }
}
