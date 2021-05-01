using System;
using System.Collections.Generic;
using System.Linq;
namespace task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            // When you write code, you should assign values to these lists
            List<Student> list1 = new List<Student> 
            { new Student {name ="Robin",  id = 4},
              new Student {name ="yousuf", id = 3 },
              new Student {name ="mashuq", id = 2},
              new Student {name ="shakil", id = 1},
             
            };
            List<Student> list2 = new List<Student>
            { new Student {name ="jafor",    id = 9},
              new Student {name ="jabbar",   id = 8 },
              new Student {name ="kobir",    id = 7},
              new Student {name ="mostafiz", id = 6},
            };
            List<string> result = from a in list1
                                  join b in list2
                                  on a.id equals b.id
                                  select new
                                  {

                                  };
            foreach (var item in result)
            {
                Console.WriteLine();
            }
        }
    }
    public class Student
    {
        public string name { get; set; }
        public int id { get; set; }

    }
}
