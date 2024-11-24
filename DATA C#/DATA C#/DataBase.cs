using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Windows.Forms;

namespace DATA_C_
{
    internal class DataBase
    {
        private static DataBase _instance;
        private SQLiteConnection _connection;

        private DataBase()
        {
            // Убедитесь, что поле класса connection инициализировано, а не локальная переменная
            _connection = new SQLiteConnection("Data Source=C:\\Users\\Lemon_Russ\\Desktop\\Курсач\\first2.db");
        }

        public static DataBase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataBase();
            }
            return _instance;
        }

        public SQLiteConnection GetConnection()
        {
            // Открываем соединение, если оно еще не открыто
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}

