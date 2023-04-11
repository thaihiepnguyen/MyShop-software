using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace MyShop.DAO
{

    // TODO: bắt lỗi exception khi không kết nối DB được => Xử lý App như thế nào?
    class DatabaseUtilitites
    {
        private string _server;
        private string _databaseName;
        private string _user;
        private string _password;

        private static DatabaseUtilitites _instance = null;
        SqlConnection _connection;
        public static DatabaseUtilitites getInstance()
        {
            if (_instance == null)
            {
                _instance = new DatabaseUtilitites();
            }
            return _instance;
        }

        public DatabaseUtilitites() {
            _server = "sqlexpress";
            _databaseName = "RawDB";
            _user = "admin";
            _password = "admin";
            _connection = null;
        }

        public DatabaseUtilitites(string server, string databaseName, string user, string password)
        {
            _server = server;
            _databaseName = databaseName;
            _user = user;
            _password = password;

            string connectionString = $"""
                Server = .\{server};
                User ID = {user}; Password={password};
                Database = {databaseName};
                TrustServerCertificate=True
                """;

            _connection = new SqlConnection(connectionString);

            try
            {
                _connection.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Cannot connect to database. Reason: {ex.Message}");
            }

            _instance = this;
        }

        public SqlConnection connection { get { return _connection; } }
    }
}
