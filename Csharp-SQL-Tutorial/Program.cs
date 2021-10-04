using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

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
            var students = new List<Student>();
            while (reader.Read())
            {   //starts at type in sql / becomes an object in the pipeline / must be turned into the data type desired
                var student = new Student();
                student.Id = Convert.ToInt32(reader["Id"]);
                student.Firstname = reader["Firstname"].ToString();
                student.Lastname = Convert.ToString(reader["Lastname"]);
                student.StateCode = reader["Statecode"].ToString();
                //This Column is allow to be null so it needs to be checked and handled
                student.SAT = reader["SAT"].Equals(DBNull.Value)
                    //if true its a nullable int
                    ? (int?)null
                    //if false make the now object an int for C#
                    : Convert.ToInt32(reader["SAT"]);
                student.GPA = Convert.ToDecimal(reader["GPA"]);
                student.MajorId = reader["MajorId"].Equals(DBNull.Value)
                    //if true its a nullable int
                    ? (int?)null
                    //if false make the now object an int for C#
                    : Convert.ToInt32(reader["MajorId"]);

                Console.WriteLine(student);
                students.Add(student);
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
