using Microsoft.Data.SqlClient;
using System;

namespace Csharp_SQL_Tutorial
{
    class Program
    {
        static void Main(string[] args)                                       //SQL connection open and closure example
        {
            var connStr = "server=localhost\\sqlexpress;database=PrsDb;trusted_connection=true;";
            var sqlConn = new SqlConnection(connStr);
            sqlConn.Open();               //Makes connection
            if(sqlConn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open");
                return;
            }
            Console.WriteLine("Connection opened!");


            // continue sql here


            sqlConn.Close();
            if(sqlConn.State != System.Data.ConnectionState.Closed)
            {
                Console.WriteLine("Connection closure failed!!");
                return;
            }
            Console.WriteLine("Connection closure success!!");
        }
    }
}
