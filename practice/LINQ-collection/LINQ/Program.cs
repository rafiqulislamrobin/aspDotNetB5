using System;
using System.Collections.Generic;
using System.Linq;
namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = new List<string>() { "falcon", "eagle", "sky", "tree", "water" };

            //element access
            Console.WriteLine(words.Last(word => word.Length == 3));
            Console.WriteLine();




            Console.WriteLine("containing A");
            var result1 = from word in words
                         where word.Contains("a")
                         select word;

            foreach (var item in result1)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();



            //or



            Console.WriteLine("containing E");

            var result2 = words.Where(word => word.Contains("e"));
            foreach(var item in result2)
            {
                Console.WriteLine(item);
            }



            //LINQ SelectMany
            Console.WriteLine();

            int[][] vals = {
              new[] {1, 2, 3},
              new[] {4},
              new[] {5, 6, 6, 2, 7, 8},
                 };

            var result3 = vals.SelectMany(array => array).OrderBy(x => x);
            Console.WriteLine(string.Join(",",result3));



            //LINQ Concat
            
            Console.WriteLine();
            List<string> name = new List<string>() { "akash", "murad", "arif", "zaltan" };
            List<string> id = new List<string>() { "1", "4", "3", "2" };

            var result4 = name.Concat(id);
            foreach (var item in result4)
            {
                Console.Write($"{item} ");
            }


            //orderby
            Console.WriteLine("\n \n");
            var result5 = from i in id
                          orderby id
                          select i;
            foreach (var item in result5)
            {
                Console.Write(item+" " );
            }
          
        }
    }
}
