using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new();
            var name = person?.Name ?? "Name is null";
            Console.WriteLine(name);
        }
    }
    public class Person
    {
        public string Name =null ;
    }
}
