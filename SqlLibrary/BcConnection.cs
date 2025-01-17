﻿using System;
using System.Data.SqlClient;

namespace SqlLibrary {
    public class BcConnection {
        public SqlConnection Connection { get; set; }

        public void Connect(string server, string database, string auth) {
            var connStr = $"server={server};database={database};{auth}";
            Connection = new SqlConnection(connStr);
            Connection.Open();
            if(Connection.State != System.Data.ConnectionState.Open) {
                Console.WriteLine("Could not open connection -- check connection string");
                Connection = null;
                return;
            }
            Console.WriteLine("Connection open");
        }
        public void Disconnect() {
            if(Connection == null) {
                return;
            }
            if(Connection.State == System.Data.ConnectionState.Open) {
                Connection.Close();
                Connection.Dispose();
                Connection = null;
            }
        }
    }
}
