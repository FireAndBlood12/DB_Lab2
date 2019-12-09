using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database
{
    public class DBConnection
    {
        NpgsqlConnection connection;

        private static DBConnection instance;

        private DBConnection()
        {
            connection
            = new NpgsqlConnection("Server=127.0.0.1; Port=5432; User Id=postgres; Password=alex1204ata; Database=school;");
        }

        public static DBConnection getInstance()
        {
            if(instance == null) {
                instance = new DBConnection();
            }
            return instance;
        }

        public NpgsqlConnection Open()
        {
            connection.Open();
            return connection;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
