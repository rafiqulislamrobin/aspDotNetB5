using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TASK_1
{
    class Program
    {
        static void Main(string[] args)
        {

            IList<string> pathlist = new List<string>();

            var path1 = @"D:\ASPDOTNET\aspDotNetB5\t1.txt";
            pathlist.Add(path1);
            var path2 = @"D:\ASPDOTNET\aspDotNetB5\t2.txt";
            pathlist.Add(path2);



            var fileprocess = new FileProcessor();
            var thread1 = new Thread(new ThreadStart(()=>fileprocess.ReadFiles(pathlist)));

            thread1.Start();
        }
    }
}
