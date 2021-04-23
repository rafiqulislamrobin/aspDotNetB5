using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK_4
{
   
    public static class Demo
    {
        public class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }


        public static void Main()
        {
       
            List<Student> list1 = new List<Student> {
                new Student { Name ="robin", Age=25 },
                new Student { Name ="ashiq", Age=26 },
                new Student { Name ="yousuf", Age=27 },
                new Student { Name ="kratos", Age=25 }
            };
        


            List<Student> list2 = new List<Student> { new Student { Name ="roni", Age=25 },
                new Student { Name ="rahim", Age=26 },
                new Student { Name ="karim", Age=27 },
                new Student { Name ="jafor", Age=25 } };

           List<string> result = list1.Concat(list2).OrderBy(name => name.Name).OrderBy(age => age.Age).Select(get => get.Name).ToList();

            foreach (var studentName in result)
            {
                Console.WriteLine(studentName);
            }
        }
    }
}


