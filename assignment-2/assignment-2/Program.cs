using System;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            using SqlConnection connection = new();
            connection.ConnectionString = "Server = DESKTOP-QO6CDJ4\\SQLEXPRESS; Database = Studentdb; Uid = Sa; Pwd = allahhelpme;";

            var myorm = new MyORM<student>(connection);

            student Student = new();
            //Student.name = "Rafiq";
            //Student.amount = 500;

            //myorm.Insert(Student);


            //Student.amount = 900;
            //Student.name = "rq";
            //Student.Id = 3;

            myorm.Delete(5);
            Console.WriteLine("complete");

        }
    
       
    }
}
