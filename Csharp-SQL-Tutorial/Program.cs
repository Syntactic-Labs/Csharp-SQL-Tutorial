using Microsoft.Data.SqlClient;
using System;

namespace Csharp_SQL_Tutorial
{
    class Program
    {
        static void Main(string[] args)                                       /*SQL connection open and closure example*/
        {   //places the address in a variable to use in our code
            var connStr = "server=localhost\\sqlexpress;database=EdDb;trusted_connection=true;";
            //creates connection  (variable containing the address)
            var sqlConn = new SqlConnection(connStr);
            //Opens connection
            sqlConn.Open();               
            if(sqlConn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open");
                return;
            }
            Console.WriteLine("Connection opened!");
            //SQL CODE!!
            var sql = "Select * from Student;";
            var cmd = new SqlCommand(sql, sqlConn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {   //starts at type in sql / becomes an object in the pipeline / must be turned into the data type desired
                var Id = Convert.ToInt32(reader["Id"]);
                var Firstname = reader["Firstname"].ToString();
                var Lastname = Convert.ToString(reader["Lastname"]);
                var Statecode = reader["Statecode"].ToString();
                //This Column is allow to be null so it needs to be checked and handled
                var SAT = reader["SAT"].Equals(DBNull.Value)
                    //if true its a nullable int
                    ? (int?)null
                    //if false make the now object an int for C#
                    : Convert.ToInt32(reader["SAT"]);
                var GPA = Convert.ToDecimal(reader["GPA"]);
                var Message = $"{Id} | {Firstname} | {Lastname} | {Statecode}" +
                            $" | {(SAT != null ? SAT : "NULL")} | {GPA}";
                Console.WriteLine(Message);
            }
            reader.Close();

            //ALWAYS close the connection so others can use it.
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
