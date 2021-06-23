using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;


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


            Student.Id = 2;
            Student.name = "Rafiq";
            Student.amount = 500;

            myorm.Insert(Student);
            myorm.Update(Student);
            myorm.Delete(Student);
            myorm.Delete(2);

            var students = myorm.GetAll();  //getall
            All(students);


            var std = myorm.GetById(2);   //by id
            ById(std);


            Console.WriteLine("complete");

        }
    
       public static void All(IList<student> students)
        {
            foreach (var student in students)
            {
                var type = student.GetType();
                var prop = type.GetProperties();
                foreach (var p in prop)
                {
                    Console.Write($"{p.Name}={p.GetValue(student)} ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static void ById(student std)
        {
            var type = std.GetType();
            var prop = type.GetProperties();

            foreach (var p in prop)
            {
                Console.Write($"{p.Name}={p.GetValue(std)} ");
            }
            Console.WriteLine();
        }
    }
}
