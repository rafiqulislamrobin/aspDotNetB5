using System;

namespace practice
{
    class Program
    {
        static void Main(string[] args)
        {
            int t =int.Parse(Console.ReadLine());
            while (t != 0)
            {
                string[] input = Console.ReadLine().Split(' ');

                decimal r = decimal.Parse(input[0]);
                decimal b = decimal.Parse(input[1]);
                decimal d = decimal.Parse(input[2]);

               
               

                
                if (r>=1 && b>=1 &&(Math.Ceiling(r/b)- Math.Ceiling(b / b)<=d))
                {
                    Console.WriteLine("YES");
                }
                else
                {
                    
                        Console.WriteLine("NO");
                    
                }
               
                t--;
            }
        }
    }
}
