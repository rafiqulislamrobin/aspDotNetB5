using System;
using System.Threading;
using System.IO;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var program = new Program();
            var thread2 = new Thread((new ThreadStart(()=> FileRead("hello jello"))));
            //var thread1 = new Thread(new ThreadStart( FileRead)));
            //thread1.Start();
            thread2.Start();
            
        }
        void NumberS1()
        {
            for (int i = 0; i < 100; i++)
            {
                if (i%2==1)
                {
                    Console.WriteLine($"Number: {i} ");
                    Thread.Sleep(100);
                }
    

            }

        }
        static void NumberS2()
        {
            for (int i = 0; i < 100; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine($"Number: {i} ");
                    Thread.Sleep(100);
                    
                }

            }
        }
        public static  void FileRead(string message)
        {
            var path = @"D:\ASPDOTNET\practice\file.txt";
            lock (path)
            {
                File.WriteAllText(path, message);
            }
        }
    }
}
